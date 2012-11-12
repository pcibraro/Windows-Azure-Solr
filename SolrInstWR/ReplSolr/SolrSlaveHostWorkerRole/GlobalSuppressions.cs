using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.CodeAnalysis;

/*We can suppress these issues because the library is not intended to be used like api*/
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "SolrSlaveHostWorkerRole.WorkerRole.#OnStop()")]
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "SolrSlaveHostWorkerRole.WorkerRole.#OnStart()")]
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "SolrSlaveHostWorkerRole.WorkerRole.#Log(System.String,System.String)")]
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "SolrSlaveHostWorkerRole.WorkerRole.#ExecuteShellCommand(System.String,System.Boolean,System.String)")]
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "SolrSlaveHostWorkerRole.WorkerRole.#InitRoleInfo()")]
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "SolrSlaveHostWorkerRole.WorkerRole.#Run()")]
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "SolrSlaveHostWorkerRole.WorkerRole.#CreateSolrStorageVhd()")]
[module: SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "SolrSlaveHostWorkerRole.WorkerRole.#StartSolr()")]