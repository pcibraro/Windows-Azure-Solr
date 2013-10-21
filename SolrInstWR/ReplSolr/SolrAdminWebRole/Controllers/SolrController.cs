using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SolrAdminWebRole.Controllers
{
    public class SolrController : AsyncController
    {
        protected override void HandleUnknownAction(string actionName)
        {
            if (!Request.HttpMethod.Equals("get", StringComparison.InvariantCultureIgnoreCase) ||
                actionName.Equals("Index", StringComparison.InvariantCultureIgnoreCase))
            {
                base.HandleUnknownAction(actionName);
            }

            var uri = Request.RawUrl.ToLower();
            var index = uri.IndexOf("/solr/");

            var remainingUri = uri.Substring(index + "/solr/".Length);

            var solrUrl = HelperLib.Util.GetSolrEndpoint(false);

            if (solrUrl == null)
            {
                this.HttpNotFound().ExecuteResult(this.ControllerContext);
                return;
            }

            var httpRequest = (HttpWebRequest)WebRequest.Create(solrUrl + remainingUri);
            try
            {
                using (var solrResponse = (HttpWebResponse)httpRequest.GetResponse())
                {
                    using (var stream = solrResponse.GetResponseStream())
                    {
                        if(!string.IsNullOrWhiteSpace(solrResponse.ContentEncoding))
                            Response.ContentEncoding = Encoding.GetEncoding(solrResponse.ContentEncoding);

                        if(!string.IsNullOrWhiteSpace(solrResponse.ContentType))
                            Response.ContentType = solrResponse.ContentType;

                        stream.CopyTo(Response.OutputStream);
                    }
                }
            }
            catch (WebException)
            {
                new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError, "Can not forward request to solr")
                    .ExecuteResult(this.ControllerContext);
            }
        }
       

    }
}
