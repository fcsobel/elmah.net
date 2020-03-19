using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.Net.Logger.Data
{
	public partial class LoggerContext : DbContext
	{
		public ISiteInstance siteInstance { get; set; }


		static LoggerContext()
		{
			Database.SetInitializer<LoggerContext>(null);
		}

		//public LoggerContext()	: base("Name=LoggerContext")
        public LoggerContext(ISiteInstance siteInstance) : base(siteInstance.Site == null ? null : siteInstance.Site.ConnectionString)
        {
			this.siteInstance = siteInstance;
            this.Configuration.LazyLoadingEnabled = false;
		}

		public DbSet<Log> Logs { get; set; }
		public DbSet<LogApplication> LogApplications { get; set; }
		public DbSet<LogUser> Users { get; set; }
		public DbSet<LogMessageSource> MessageSources { get; set; }
		public DbSet<LogMessageType> MessageTypes { get; set; }
		public DbSet<LogMessage> LogMessages { get; set; }
		public DbSet<LogMessageDetail> MessageDetails { get; set; }
        public DbSet<Filter> Filters { get; set; }
		//public DbSet<Role> Roles{ get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new LogMap());
			modelBuilder.Configurations.Add(new LogApplicationMap());			
			modelBuilder.Configurations.Add(new LogMessageMap());
			modelBuilder.Configurations.Add(new LogUserMap());
			modelBuilder.Configurations.Add(new LogMessageTypeMap());
			modelBuilder.Configurations.Add(new LogMessageSourceMap());
			modelBuilder.Configurations.Add(new LogMessageDetailMap());
            modelBuilder.Configurations.Add(new FilterMap());
			//modelBuilder.Configurations.Add(new RoleMap());
		}
    }

	//public class DBInitializer : DropCreateDatabaseIfModelChanges<LoggerContext>
	public class DBInitializer : CreateDatabaseIfNotExists<LoggerContext>
	{
		protected override void Seed(LoggerContext context)
		{
			base.Seed(context);
		}
	}
}
