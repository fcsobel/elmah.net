using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace c3o.Logger.Data
{
	// Logs
	public class LogMap : EntityTypeConfiguration<Log>
    {
        public LogMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
			this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("Logs");
			this.Property(t => t.Id).HasColumnName("Id");            
			this.Property(t => t.Name).HasColumnName("Name");

        }
    }

    public class FilterMap : EntityTypeConfiguration<Filter>
    {
        public FilterMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("Filters");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");

            // link to log
            this.HasMany(c => c.FilterTypes)
                .WithMany(x=>x.Filters)
                .Map(m =>
                {
                    m.MapLeftKey("FilterId");
                    m.MapRightKey("TypeId");
                    m.ToTable("FilterTypes");
                });

            // link to log
            this.HasMany(c => c.FilterSources)
                .WithMany(x => x.Filters)
                .Map(m =>
                {
                    m.MapLeftKey("FilterId");
                    m.MapRightKey("SourceId");
                    m.ToTable("FilterSources");
                });


            //this.HasRequired(t => t.Log).WithMany(t => t.Applications).HasForeignKey(d => d.LogId);

        }
    }

}