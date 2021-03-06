﻿#region Copyright Notice
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

namespace HelperLib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.WindowsAzure.StorageClient;
    using Microsoft.WindowsAzure.ServiceRuntime;
    using System.Net;
    using System.Globalization;

    public sealed class RoleInfoEntity : TableServiceEntity
    {
        private const string partitionKey = "RoleEndPoints";
        public RoleInfoEntity()
        {
            this.PartitionKey = partitionKey;
            RowKey = string.Format(CultureInfo.InvariantCulture, "{0:10}_{1}", DateTime.MaxValue.Ticks - DateTime.Now.Ticks, Guid.NewGuid());
        }

        // Define the properties that Role information.
        public string RoleId { get; set; }
        public bool IsSolrMaster { get; set; }
        public string IPString { get; set; }
        public int Port { get; set; }
    }
}
