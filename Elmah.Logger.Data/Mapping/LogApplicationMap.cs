using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Elmah.Net.Logger.Data
{
	// Applications
	public class LogApplicationMap : EntityTypeConfiguration<LogApplication>
    {
        public LogApplicationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
			this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("LogApplications");
			this.Property(t => t.Id).HasColumnName("Id");            
			this.Property(t => t.Name).HasColumnName("Name");

			// link to log
			this.HasRequired(t => t.Log).WithMany(t => t.Applications).HasForeignKey(d => d.LogId);
        }
    }
}