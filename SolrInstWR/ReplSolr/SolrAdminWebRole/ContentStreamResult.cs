using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SolrAdminWebRole
{
    public class ContentStreamResult : ActionResult
    {
        public string ContentEncoding { get; set; }
        public string ContentType { get; set; }
        public Stream Response { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (!string.IsNullOrWhiteSpace(ContentEncoding))
                context.HttpContext.Response.ContentEncoding = Encoding.GetEncoding(ContentEncoding);

            if (!string.IsNullOrWhiteSpace(ContentType))
                context.HttpContext.Response.ContentType = ContentType;

            Response.CopyTo(context.HttpContext.Response.OutputStream);
        }
    }
}