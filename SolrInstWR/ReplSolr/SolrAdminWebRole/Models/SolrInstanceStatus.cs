using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolrAdminWebRole.Models
{
    public class SolrInstanceStatus
    {
        public string InstanceName { get; set; }
        public string Endpoint { get; set; }
        public bool IsReady { get; set; }
        public bool IsMaster { get; set; }
        public string IndexVersion { get; set; }
        public string Generation { get; set; }
        public string IndexSize { get; set; }
        public string NumTimesReplicated { get; set; }
        public string LastReplicationTime { get; set; }
        public string NextReplicationTime { get; set; }
        public string PollInterval { get; set; }
    }
}