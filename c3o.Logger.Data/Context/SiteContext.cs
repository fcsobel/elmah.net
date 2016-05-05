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
    }

    public class SiteContext
    {
        public SiteRecord Site { get; set; }

        public List<SiteRecord> Sites = new List<SiteRecord>()
        {
            new SiteRecord("www", "www"),
            new SiteRecord("acme", "acme"),
        };

        /// <summary>
        /// Init Site
        /// </summary>
        public SiteContext()
        {
            // calculate site bases on subdomain
            var name = this.SiteName;
            if (!string.IsNullOrWhiteSpace(name))
            {
                this.Site = this.Sites.First(x => x.Subdomain == name);
            }
            if (this.Site == null)
            {
                this.Site = Sites.First();
            }
        }

        /// <summary>
        /// Getter
        /// </summary>
        public static SiteContext Current
        {
            get
            {
                SiteContext context = (SiteContext)HttpContext.Current.Items["SiteContext"];
                if (context == null)
                {
                    context = new SiteContext();
                    HttpContext.Current.Items["SiteContext"] = context;
                }
                return context;
            }
        }
   

        protected string SiteName
        {
            get
            {
                var context = HttpContext.Current;

                if (context != null && context.Request != null)
                {
                    var host = context.Request.Url.Host;
                    
                    if (host.EndsWith(".elmah.net"))
                    {
                        return host.Remove(host.Length - ".elmah.net".Length, ".elmah.net".Length);
                    }
                }             

                // default to acme   
                return "acme";
            }
        }

        public void CreateDB()
        {
            var site = this.Site;

            if (site != null)
            {
                // path to scripts
                var path = HttpContext.Current.Server.MapPath("~/_deploy/sql/0.00.001");

                if (Directory.Exists(path))
                {
                    // configure 
                    var upgrader = DeployChanges.To
                            .SqlDatabase(site.ConnectionString)
                            .WithScriptsFromFileSystem(path)
                            //.WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                            //.LogToConsole()
                            .Build();

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