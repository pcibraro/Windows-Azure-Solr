using System;
using System.Collections.Generic;
using System.IO;
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
        [BasicAuthentication]
        public async Task<ActionResult> HandleRequest()
        {
            var uri = Request.RawUrl;
            var index = uri.IndexOf("/solr/", StringComparison.InvariantCultureIgnoreCase);

            var remainingUri = uri.Substring(index + "/solr/".Length);

            var solrUrl = HelperLib.Util.GetSolrEndpoint(false);

            if (solrUrl == null)
            {
                return new HttpNotFoundResult();
            }

            var httpRequest = (HttpWebRequest)WebRequest.Create(solrUrl + remainingUri);
            try
            {
                using (var solrResponse = (HttpWebResponse) await httpRequest.GetResponseAsync())
                {
                    using (var stream = solrResponse.GetResponseStream())
                    using (var sr = new StreamReader(stream, Encoding.UTF8))
                    {
                        var content = await sr.ReadToEndAsync();

                        return new ContentResult
                        {
                            Content = content,
                            ContentEncoding = Encoding.UTF8,
                            ContentType = solrResponse.ContentType
                        };
                    }
                }
            }
            catch (WebException)
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError, 
                    "Can not forward request to solr");
            }
        }
    }
}
