//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace c3o.Logger.Data
//{
//	public partial class AccountContext : DbContext
//	{
//		static AccountContext()
//		{
//			Database.SetInitializer<AccountContext>(null);
//		}

//		//public LoggerContext()	: base("Name=LoggerContext")
//        public AccountContext(SiteContext siteContext) : base()
//        {
//            this.Configuration.LazyLoadingEnabled = false;
//		}

//		public DbSet<Log> Logs { get; set; }

//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//		{
//			modelBuilder.Configurations.Add(new LogMap());
//        }
//    }	
//}
