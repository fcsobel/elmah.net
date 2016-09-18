//using c3o.Site.Data;
//using DbUp;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;

//namespace c3o.Logger.Data
//{
//	public class SiteInstance
//	{
//		private SiteList SiteList { get; set; }
//		private SiteContext db { get; set; }

//		public SiteRecord Site { get; set; }

//		/// <summary>
//		/// Init Site
//		/// </summary>
//		public SiteInstance(SiteList siteList, Site.Data.SiteContext siteContext)
//		{
//			this.SiteList = siteList;
//			this.db = siteContext;

//			// calculate site bases on subdomain
//			var name = this.SiteName;
//			if (!string.IsNullOrWhiteSpace(name))
//			{
//				this.Site = this.SiteList.Sites.FirstOrDefault(x => x.Subdomain == name);
//			}
//			//if (this.Site == null)
//			//{
//			//    this.Site = this.SiteInstance.Sites.First();
//			//}
//		}

//		///// <summary>
//		///// Getter
//		///// </summary>
//		//public static SiteContext Current
//		//{
//		//    get
//		//    {
//		//        SiteContext context = (SiteContext)HttpContext.Current.Items["SiteContext"];
//		//        if (context == null)
//		//        {
//		//            context = new SiteContext();
//		//            HttpContext.Current.Items["SiteContext"] = context;
//		//        }
//		//        return context;
//		//    }☺
//		//}

//		//// create new site record and check db          
//		//public void CreateSite()
//		//{
//		//    if (this.Site == null)
//		//    {
//		//        var siteName = this.SiteName;

//		//        if (!string.IsNullOrWhiteSpace(siteName))
//		//        {
//		//            SiteRecord site = new SiteRecord(siteName, siteName);

//		//            if (site.CheckSiteDb())
//		//            {
//		//                db.Sites.Add(new Site.Data.Site() { Name = site.Subdomain, Url = site.Subdomain + ".elmah.net" });
//		//                db.SaveChanges();

//		//                // add siteinstance
//		//                this.SiteInstance.Add(site);
//		//                this.Site = site;
//		//            }
//		//        }
//		//    }
//		//}


//		public string SiteName
//		{
//			get
//			{
//				var context = HttpContext.Current;

//				try
//				{
//					if (context != null && context.Request != null)
//					{
//						var host = context.Request.Url.Host;

//						if (host.EndsWith(".elmah.net"))
//						{
//							var name = host.Remove(host.Length - ".elmah.net".Length, ".elmah.net".Length);

//							if (name.Contains("-"))
//							{
//								return name.Split('-')[1];
//							}
//							else
//							{
//								return name;
//							}
//						}
//					}
//				}
//				catch
//				{

//				}
//				// default to main site
//				return "www";
//			}
//		}

//		//public void CreateDb()
//		//{
//		//    var conn = System.Configuration.ConfigurationManager.ConnectionStrings["SiteContext"];

//		//    var site = this.Site;

//		//    if (site != null)
//		//    {www.
//		//        // path to scripts
//		//        var path = HttpContext.Current.Server.MapPath("~/_deploy/sql/0.00.004");

//		//        if (Directory.Exists(path))
//		//        {
//		//            // configure 
//		//            var upgrader = DeployChanges.To
//		//                    .SqlDatabase(conn.ConnectionString)
//		//                    .WithScriptsFromFileSystem(path)
//		//                    //.WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
//		//                    //.LogToConsole()
//		//                    .Build();

//		//            if (upgrader.IsUpgradeRequired())
//		//            {
//		//                var response = upgrader.PerformUpgrade();
//		//                if (response.Successful)
//		//                {

//		//                }
//		//                else
//		//                {

//		//                }
//		//            }
//		//        }
//		//    }
//		//}

//	}
//}