//using c3o.Site.Data;
//using DbUp;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;

//namespace c3o.Logger.Data
//{
//	public class SiteList
//	{
//		//private SharedContext db { get; set; }
//		public List<SiteRecord> Sites { get; set; }

//		public SiteList()
//		{
//			//SharedContext context
//			//this.db = context;

//			//var list = this.db.Sites.ToList();
//			//this.Sites =  list.Select(x => new SiteRecord(x.Name, x.Name)).ToList();
//		}

//		//public void Refresh(SharedContext context)
//		//{
//		//    var list = context.Sites.ToList();
//		//    this.Sites = list.Select(x => new SiteRecord(x.Name, x.Name)).ToList();
//		//}

//		//public List<SiteRecord> Sites
//		//{
//		//    get
//		//    {
//		//        var list = this.db.Sites.ToList();
//		//        return list.Select(x => new SiteRecord(x.Name, x.Name)).ToList();
//		//    }
//		//}

//		public void Add(SiteRecord obj)
//		{
//			this.Sites.Add(obj);

//			//db.Sites.Add(new Site.Data.Site() { Name = obj.Subdomain, Url = obj.Subdomain + ".elmah.net" });
//			//db.SaveChanges();

//			//var list = this.db.Sites.ToList();
//			//this.Sites = list.Select(x => new SiteRecord(x.Name, x.Name)).ToList();
//		}

//		//public List<SiteRecord> Sites = new List<SiteRecord>()
//		//{
//		//    new SiteRecord("www", "www"),
//		//    new SiteRecord("acme", "acme"),
//		//};
//	}
//}