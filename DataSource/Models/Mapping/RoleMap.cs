using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataSource.Models.Mapping
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            // Primary Key
            this.HasKey(t => t.RoleID);

            // Properties
            this.Property(t => t.RoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RoleName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Role");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.RoleName).HasColumnName("RoleName");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
