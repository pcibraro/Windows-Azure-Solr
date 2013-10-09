using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace SolrProxy.Controllers
{
    public class SolrController : ApiController
    {
        public readonly string[] ProtectedResources = new string[] { "dataimport" };

        public async Task<HttpResponseMessage> Get(HttpRequestMessage request)
        {
            var uri = request.RequestUri.AbsoluteUri.ToLower();
            var index = uri.IndexOf("api/solr/");
            
            var remainingUri = uri.Substring(index + "api/solr/".Length);

            if(ProtectedResources.Any(p => remainingUri.StartsWith(p) &&
                !User.Identity.IsAuthenticated))
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            var solrUrl = HelperLib.Util.GetSolrEndpoint(false);

            if (solrUrl == null)
                return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable);

            var client = new HttpClient();
            var solrResponse = await client.GetAsync(solrUrl + remainingUri);

            var response = new HttpResponseMessage();
            response.StatusCode = solrResponse.StatusCode;
            response.Content = solrResponse.Content;

            return response;
        }
    }
}
