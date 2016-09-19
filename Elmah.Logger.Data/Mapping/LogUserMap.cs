using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace c3o.Logger.Data
{
	// Users
	public class LogUserMap : EntityTypeConfiguration<LogUser>
    {
        public LogUserMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
			this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("LogUsers");
			this.Property(t => t.Id).HasColumnName("Id");            
			this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}