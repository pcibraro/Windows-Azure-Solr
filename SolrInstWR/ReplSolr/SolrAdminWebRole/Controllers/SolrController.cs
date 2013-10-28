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
    public class SolrController : Controller
    {
        [BasicAuthentication]
        public ActionResult HandleRequest()
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
                using (var solrResponse = (HttpWebResponse)httpRequest.GetResponse())
                {
                    using (var stream = solrResponse.GetResponseStream())
                    {

                        return new ContentStreamResult
                        {
                            ContentEncoding = solrResponse.ContentEncoding,
                            ContentType = solrResponse.ContentType,
                            Response = stream
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
