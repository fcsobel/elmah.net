using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c3o.Logger.Data
{
    public class SiteRecord
    {
        public string Subdomain { get; set; }
        public string Database { get; set; }
        public string ConnectionString { get; set; }

        public SiteRecord(string subdomain, string database)
        {
            this.Subdomain = subdomain;
            this.Database = database;

            // Init Database
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["LoggerContext"];

            // connection string 
            if (connection != null)
            {
                this.ConnectionString = string.Format(connection.ConnectionString, this.Database);
            }
        }
    }
}
