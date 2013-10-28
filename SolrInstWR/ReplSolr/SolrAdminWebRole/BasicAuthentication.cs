using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace SolrAdminWebRole
{
    public class BasicAuthentication : ActionFilterAttribute
    {
        string username;
        string password;

        public BasicAuthentication()
        {
            if (RoleEnvironment.IsAvailable)
            {
                var credentials = RoleEnvironment.GetConfigurationSettingValue("BasicAuthCredentials");
                if (credentials != null)
                {
                    var usernameAndPassword = credentials.Split(';');
                    username = usernameAndPassword[0];
                    password = usernameAndPassword[1];
                }
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!string.IsNullOrWhiteSpace(username) &&
                !string.IsNullOrWhiteSpace(password))
            {
                var req = filterContext.HttpContext.Request;
                if (!String.IsNullOrEmpty(req.Headers["Authorization"]))
                {
                    var credentials = System.Text.ASCIIEncoding.ASCII
                                .GetString(Convert.FromBase64String(req.Headers["Authorization"].Substring(6)))
                                .Split(':');
                    var user = new { Name = credentials[0], Password = credentials[1] };
                    if (!(user.Name == username && user.Password == password))
                    {
                        filterContext.Result = new HttpStatusCodeResult(401);
                    }
                }
                else
                {
                     var host = filterContext.HttpContext.Request.Url.DnsSafeHost;                    
                     filterContext.HttpContext.Response.Headers
                        .Add("www-authenticate", string.Format("Basic realm=\"{0}\"", host));

                     filterContext.Result = new HttpStatusCodeResult(401);
                }
            }
        }
    }
}