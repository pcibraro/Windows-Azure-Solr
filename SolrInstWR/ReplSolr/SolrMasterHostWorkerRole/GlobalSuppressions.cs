using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.CodeAnalysis;

/*We can suppress these issues because the library is not intended to be used like api*/
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "SolrMasterHostWorkerRole.WorkerRole.#OnStop()")]
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "SolrMasterHostWorkerRole.WorkerRole.#OnStart()")]
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "SolrMasterHostWorkerRole.WorkerRole.#Log(System.String,System.String)")]
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "SolrMasterHostWorkerRole.WorkerRole.#ExecuteShellCommand(System.String,System.Boolean,System.String)")]
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "SolrMasterHostWorkerRole.WorkerRole.#InitRoleInfo()")]
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "SolrMasterHostWorkerRole.WorkerRole.#Run()")]
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "SolrMasterHostWorkerRole.WorkerRole.#CreateSolrStorageVhd()")]
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "SolrMasterHostWorkerRole.WorkerRole.#StartSolr()")]