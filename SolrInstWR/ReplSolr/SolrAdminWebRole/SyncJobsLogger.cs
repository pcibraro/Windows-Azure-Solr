using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolrAdminWebRole
{
    public class SyncJobsLogger
    {
        const string SyncResulsTableName = "SyncJobs";

        string connectionString;

        public SyncJobsLogger(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void CreateTableIfNotExists()
        {

            var account = CloudStorageAccount.Parse(this.connectionString);
            account.CreateCloudTableClient().CreateTableIfNotExist(SyncResulsTableName);

        }

        public bool SaveJob(bool status, string description)
        {


            var context = CloudStorageAccount.Parse(this.connectionString)
                .CreateCloudTableClient()
                .GetDataServiceContext();

            context.AddObject(SyncResulsTableName, new SyncJob
                {
                    Date = DateTime.UtcNow,
                    Status = status,
                    PartitionKey = "Delta",
                    Description = description,
                    RowKey = string.Format("{0:d10}", DateTime.MaxValue.Ticks
                        - DateTime.UtcNow.Ticks),

                });

            context.SaveChangesWithRetries();

            return true;

        }

        internal class SyncJob : TableServiceEntity
        {
            public DateTime Date { get; set; }
            public bool Status { get; set; }
            public string Description { get; set; }
        }
    }
}