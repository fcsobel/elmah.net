using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Elmah.Net.Logger.Data
{
	// Messages
    public class LogMessageMap : EntityTypeConfiguration<LogMessage>
    {
        public LogMessageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
			this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("LogMessages");
			this.Property(t => t.Id).HasColumnName("Id");            
			this.Property(t => t.ElmahId).HasColumnName("ElmahId");

			//this.Ignore(x => x.ServerVariables);
			//this.Ignore(x => x.Form);
			//this.Ignore(x => x.Cookies);
			//this.Ignore(x => x.Data);
			//this.Ignore(x => x.QueryString);

			// link to log
			this.HasRequired(t => t.Log).WithMany(x=>x.Messages).HasForeignKey(x => x.LogId);

			// link to application
			this.HasOptional(t => t.Application).WithMany(x=>x.Messages).HasForeignKey(x => x.ApplicationId);

			// link to user
			this.HasOptional(t => t.User).WithMany().HasForeignKey(x => x.UserId);

			// link to type
			this.HasOptional(t => t.MessageType).WithMany().HasForeignKey(x => x.MessageTypeId);

			// link to source
			this.HasOptional(t => t.Source).WithMany().HasForeignKey(x => x.SourceId);

			// link to detail
			this.HasOptional(t => t.Detail).WithMany().HasForeignKey(x => x.DetailId);
        }
    }

}