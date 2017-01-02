using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace c3o.Logger.Data
{
	public class RoleMap : EntityTypeConfiguration<Role>
	{
		public RoleMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			this.Property(t => t.Name)
				.IsRequired()
				.HasMaxLength(50);

			this.Property(t => t.Title)
				.HasMaxLength(250);

			// Table & Column Mappings
			this.ToTable("Role");
			this.Property(t => t.Id).HasColumnName("RoleId");
			this.Property(t => t.Name).HasColumnName("Name");
			this.Property(t => t.Title).HasColumnName("Title");
			this.Property(t => t.Description).HasColumnName("Description");

			//// Relationships
			//this.HasMany(t => t.People)
			//	.WithMany(t => t.Roles)
			//	.Map(m =>
			//		{
			//			m.ToTable("UserAccess");
			//			m.MapLeftKey("RoleId");
			//			m.MapRightKey("UserId");
			//		});
		}

	}
}