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
    public class SiteContext
    {
        private SiteInstance SiteInstance { get; set; }

        public SiteRecord Site { get; set; }

        /// <summary>
        /// Init Site
        /// </summary>
        public SiteContext(SiteInstance site)
        {
            this.SiteInstance = site;
            
            // calculate site bases on subdomain
            var name = this.SiteName;
            if (!string.IsNullOrWhiteSpace(name))
            {
                this.Site = this.SiteInstance.Sites.FirstOrDefault(x => x.Subdomain == name);
            }
            //if (this.Site == null)
            //{
            //    this.Site = this.SiteInstance.Sites.First();
            //}
        }

        ///// <summary>
        ///// Getter
        ///// </summary>
        //public static SiteContext Current
        //{
        //    get
        //    {
        //        SiteContext context = (SiteContext)HttpContext.Current.Items["SiteContext"];
        //        if (context == null)
        //        {
        //            context = new SiteContext();
        //            HttpContext.Current.Items["SiteContext"] = context;
        //        }
        //        return context;
        //    }
        //}


        public string SiteName
        {
            get
            {
                var context = HttpContext.Current;

                try {
                    if (context != null && context.Request != null)
                    {
                        var host = context.Request.Url.Host;

                        if (host.EndsWith(".elmah.net"))
                        {
                            var name = host.Remove(host.Length - ".elmah.net".Length, ".elmah.net".Length);

                            if (name.Contains("-"))
                            {
                                return name.Split('-')[1];
                            }
                            else
                            {
                                return name;
                            }
                        }
                    }
                }
                catch
                {

                }        
                // default to acme   
                return null;
            }
        }

        //public void UpdateDb()
        //{
        //    var site = this.Site;

        //    if (site != null)
        //    {
        //        // path to scripts
        //        var path = HttpContext.Current.Server.MapPath("~/_deploy/sql/0.00.004");

        //        if (Directory.Exists(path))
        //        {
        //            // configure 
        //            var upgrader = DeployChanges.To
        //                    .SqlDatabase(site.ConnectionString)
        //                    .WithScriptsFromFileSystem(path)
        //                    //.WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
        //                    //.LogToConsole()
        //                    .Build();

        //            if (upgrader.IsUpgradeRequired())
        //            {
        //                var response = upgrader.PerformUpgrade();
        //                if (response.Successful)
        //                {

        //                }
        //                else
        //                {

        //                }
        //            }
        //        }
        //    }
        //}

    }
}