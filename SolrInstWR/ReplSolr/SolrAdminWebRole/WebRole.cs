#region Copyright Notice
/*
Copyright © Microsoft Open Technologies, Inc.
All Rights Reserved
Apache 2.0 License

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

     http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.

See the Apache Version 2.0 License for specific language governing permissions and limitations under the License.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using System.Net;
using System.Threading;
using System.Diagnostics;

namespace SolrAdminWebRole
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }

        public override void Run()
        {
            var cs = RoleEnvironment.GetConfigurationSettingValue("DataConnectionString");
            var credentials = RoleEnvironment.GetConfigurationSettingValue("BasicAuthCredentials");

            var logger = new SyncJobsLogger(cs);
            logger.CreateTableIfNotExists();

            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, policy) => true; 

            while (true)
            {
                DataImport("Endpoint1", logger, credentials);

                int interval = 60;

                if (RoleEnvironment.GetConfigurationSettingValue("PollingIntervalInMinutes") != null)
                {
                    int.TryParse(RoleEnvironment.GetConfigurationSettingValue("PollingIntervalInMinutes"), out interval);
                }

                Thread.Sleep(TimeSpan.FromMinutes(interval));
            }
        }

        public static void DataImport(string endpointName, SyncJobsLogger logger, string credentials)
        {
            var endpoint = RoleEnvironment.CurrentRoleInstance
                  .InstanceEndpoints[endpointName];

            var address = "https://localhost/solr/dataimport?command=delta-import";

            try
            {
                var webClient = new WebClient();

                if (!string.IsNullOrWhiteSpace(credentials))
                {
                    var usernameAndPassword = credentials.Split(';');
                    webClient.Credentials = new NetworkCredential(usernameAndPassword[0], usernameAndPassword[1]);
                }

                var description = webClient.DownloadString(address);

                logger.SaveJob(true, description, address);
            }
            catch (Exception ex)
            {
                logger.SaveJob(false, ex.ToString(), address);
            }
        }
    }
}
