using DbUp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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


        public void UpdateDb()
        {
            if (!string.IsNullOrWhiteSpace(SiteSettings.DeploymentPath))
            {
                // path to scripts
                var path = HttpContext.Current.Server.MapPath(SiteSettings.DeploymentPath);

                if (Directory.Exists(path))
                {
                    // configure 
                    var upgrader = DeployChanges.To
                            .SqlDatabase(this.ConnectionString)
                            .WithScriptsFromFileSystem(path)
                            //.WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                            //.LogToConsole()
                            .Build();

                    if (upgrader.IsUpgradeRequired())
                    {
                        var response = upgrader.PerformUpgrade();
                        if (response.Successful)
                        {

                        }
                        else
                        {

                        }
                    }
                }
            }
        }
    }
}
