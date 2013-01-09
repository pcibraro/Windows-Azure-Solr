using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelperLib
{
    public class SolrFileLocations
    {
        private string _solrLangDir;
        private string _solrConfDir;
        private string _vhdLangDir;
        private string _vhdConfDir;
        private string _configXml;
        private string _schemaXml;

        public string SolrLangDir { get { return _solrLangDir; } }
        public string SolrConfDir { get { return _solrConfDir; } }
        public string VhdLangDir { get { return _vhdLangDir; } }
        public string VhdConfDir { get { return _vhdConfDir; } }
        public string ConfigXml { get { return _configXml; } }
        public string SchemaXml { get { return _schemaXml; } }

        public SolrFileLocations(string majorVersion)
        {
            switch (majorVersion.Trim())
            {
                case "3":
                    _solrConfDir = @"approot\Solr\example\solr\conf";
                    _solrLangDir = @"approot\Solr\example\solr\conf\lang";
                    _vhdConfDir = "conf";
                    _vhdLangDir = @"conf";
                    _configXml = @"v3\solrconfig.xml";
                    _schemaXml = @"v3\schema.xml";
                    break;
                case "4":
                    _solrConfDir = @"approot\Solr\example\solr\collection1\conf";
                    _solrLangDir = @"approot\Solr\example\solr\collection1\conf\lang";
                    _vhdConfDir = @"collection1\conf";
                    _vhdLangDir = @"collection1\conf\lang";
                    _configXml = @"v4\solrconfig.xml";
                    _schemaXml = @"v4\schema.xml";
                    break;
                default:
                    break;
            }
        }
    }
}
