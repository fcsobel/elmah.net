using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Elmah.Net.Logger.Data
{
	// Message Source
	public class LogMessageSourceMap : EntityTypeConfiguration<LogMessageSource>
    {
        public LogMessageSourceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);
			
            // Properties
			this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("LogMessageSources");
			this.Property(t => t.Id).HasColumnName("Id");            
			this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}