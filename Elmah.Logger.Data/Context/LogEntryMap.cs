using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace c3o.API.Site.Logger
{
    public class LogEntryMap : EntityTypeConfiguration<LogEntry>
    {
        public LogEntryMap()
        {
            // Primary Key
            this.HasKey(t => t.LogId);

            // Properties
			this.Property(t => t.LogId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("ErrorLog");
			this.Property(t => t.LogId).HasColumnName("Id");            
			this.Property(t => t.Id).HasColumnName("ElmahId");

			this.Ignore(x => x.ServerVariables);
			this.Ignore(x => x.Form);
			this.Ignore(x => x.Cookies);
			this.Ignore(x => x.Data);
			this.Ignore(x => x.QueryString);
        }
    }
}