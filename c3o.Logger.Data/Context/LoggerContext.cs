using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c3o.Logger.Data
{
	public partial class LoggerContext : DbContext
	{
		static LoggerContext()
		{
			Database.SetInitializer<LoggerContext>(null);
		}

		public LoggerContext()	: base("Name=LoggerContext")
		{
			//Database.SetInitializer<c3o_loggerContext>(new CreateDatabaseIfNotExists<c3o_loggerContext>());
			//Database.SetInitializer<c3o_loggerContext>(new DropCreateDatabaseIfModelChanges<c3o_loggerContext>());
			//Database.SetInitializer<c3o_loggerContext>(new DBInitializer());
			//Database.SetInitializer(new DBInitializer());
			this.Configuration.LazyLoadingEnabled = false;
		}

		public DbSet<Log> Logs { get; set; }
		public DbSet<LogApplication> LogApplications { get; set; }
		public DbSet<LogUser> Users { get; set; }
		public DbSet<LogMessageSource> MessageSources { get; set; }
		public DbSet<LogMessageType> MessageTypes { get; set; }
		public DbSet<LogMessage> LogMessages { get; set; }
		public DbSet<LogMessageDetail> MessageDetails { get; set; }
				
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new LogMap());
			modelBuilder.Configurations.Add(new LogApplicationMap());			
			modelBuilder.Configurations.Add(new LogMessageMap());
			modelBuilder.Configurations.Add(new LogUserMap());
			modelBuilder.Configurations.Add(new LogMessageTypeMap());
			modelBuilder.Configurations.Add(new LogMessageSourceMap());
			modelBuilder.Configurations.Add(new LogMessageDetailMap());
		}
	}

	//public class DBInitializer : CreateDatabaseIfNotExists<c3o_loggerContext>
	//public class DBInitializer : DropCreateDatabaseAlways<c3o_loggerContext>
	//public class DBInitializer : DropCreateDatabaseIfModelChanges<LoggerContext>
	public class DBInitializer : CreateDatabaseIfNotExists<LoggerContext>
	{
		protected override void Seed(LoggerContext context)
		{
			base.Seed(context);
		}
	}
}
