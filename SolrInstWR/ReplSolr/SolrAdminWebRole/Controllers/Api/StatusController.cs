using SolrAdminWebRole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml;

namespace SolrProxy.Controllers
{
    public class StatusController : ApiController
    {
        public async Task<HttpResponseMessage> Get()
        {
            var solrUrl = HelperLib.Util.GetSolrEndpoint(false);

            if (solrUrl == null)
                return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable);

            var statuses = new List<SolrInstanceStatus>();

            var status = await GetSolrInstanceStatus(true, -1);
            statuses.Add(status);

            int numInstances = HelperLib.Util.GetNumInstances(false);
            for (int iInstance = 0; iInstance < numInstances; iInstance++)
            {
                status = await GetSolrInstanceStatus(false, iInstance);
                statuses.Add(status);
            }

            return Request.CreateResponse(HttpStatusCode.OK, statuses);
        }

        private async Task<SolrInstanceStatus> GetSolrInstanceStatus(bool isMaster, int iInstance)
        {
            var status = new SolrInstanceStatus() { IsReady = false };

            if (isMaster)
            {
                status.InstanceName = "Solr Master";
                status.IsMaster = true;
            }
            else
            {
                status.InstanceName = "Solr Slave #" + iInstance;
                status.IsMaster = false;
            }

            var solrUrl = HelperLib.Util.GetSolrEndpoint(isMaster, iInstance);

            if (solrUrl == null)
                return status;

            status.Endpoint = solrUrl;

            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(solrUrl + "replication?command=details");
                var result = await response.Content.ReadAsStringAsync();
                
                var docResult = new XmlDocument();
                docResult.LoadXml(result);

                XmlNode node;

                node = docResult.SelectSingleNode("response/lst[@name='details']/long[@name='indexVersion']");
                if (node != null)
                    status.IndexVersion = node.InnerText;

                node = docResult.SelectSingleNode("response/lst[@name='details']/str[@name='indexSize']");
                if (node != null)
                    status.IndexSize = node.InnerText;

                node = docResult.SelectSingleNode("response/lst[@name='details']/long[@name='generation']");
                if (node != null)
                    status.Generation = node.InnerText;

                if (!isMaster)
                {
                    node = docResult.SelectSingleNode("response/lst[@name='details']/lst[@name='slave']/str[@name='timesIndexReplicated']");
                    if (node != null)
                        status.NumTimesReplicated = node.InnerText;

                    node = docResult.SelectSingleNode("response/lst[@name='details']/lst[@name='slave']/str[@name='indexReplicatedAt']");
                    if (node != null)
                        status.LastReplicationTime = node.InnerText;

                    node = docResult.SelectSingleNode("response/lst[@name='details']/lst[@name='slave']/str[@name='nextExecutionAt']");
                    if (node != null)
                        status.NextReplicationTime = node.InnerText;

                    node = docResult.SelectSingleNode("response/lst[@name='details']/lst[@name='slave']/str[@name='pollInterval']");
                    if (node != null)
                        status.PollInterval = node.InnerText;
                }

                status.IsReady = true;
            }
            catch
            {
            }

            return status;
        }
    }
}
