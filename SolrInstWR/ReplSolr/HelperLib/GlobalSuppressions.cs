using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.CodeAnalysis;

/*We can suppress these issues because the library is not intended to be used like api*/
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "HelperLib.Util.#GetSolrEndpointInfo(System.Boolean,System.Int32)")]
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "HelperLib.Util.#GetNumInstances(System.Boolean)")]
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "HelperLib.RoleInfoDataSource.#.ctor()")]