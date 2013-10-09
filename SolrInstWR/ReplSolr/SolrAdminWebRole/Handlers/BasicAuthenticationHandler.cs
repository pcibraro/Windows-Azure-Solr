using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SolrAdminWebRole.Handlers
{
    public class BasicAuthenticationHandler : DelegatingHandler
    {
        private const string WWWAuthenticateHeader = "WWW-Authenticate";

        string username;
        string password;

        public BasicAuthenticationHandler(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                               CancellationToken cancellationToken)
        {
            var auth = request.Headers.Authorization;
            if (auth != null &&
                auth.Scheme != null &&
                auth.Parameter != null &&
                auth.Scheme.Equals("Basic", StringComparison.InvariantCultureIgnoreCase))
            {
                var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(auth.Parameter));

                var tokens = decoded.Split(':');
                if (tokens.Length < 2)
                    return null;

                if (tokens[0] == this.username &&
                    tokens[1] == this.password)
                {
                    var identity = new GenericIdentity(tokens[0], "basic");
                    var principal = new GenericPrincipal(identity, null);

                    Thread.CurrentPrincipal = principal;
                    if (HttpContext.Current != null)
                        HttpContext.Current.User = principal;
                }
            }

            return base.SendAsync(request, cancellationToken)
                .ContinueWith(task =>
                {
                    var response = task.Result;
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                        Challenge(request, response);

                    return response;
                });
        }

        /// <summary>
        /// Send the Authentication Challenge request
        /// </summary>
        /// <param name="message"></param>
        /// <param name="actionContext"></param>
        void Challenge(HttpRequestMessage request, HttpResponseMessage response)
        {
            var host = request.RequestUri.DnsSafeHost;
            response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Basic", host));
        }

    }
}