using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataSource.Models.Mapping
{
    public class User_RoleMap : EntityTypeConfiguration<User_Role>
    {
        public User_RoleMap()
        {
            // Primary Key
            this.HasKey(t => new { t.RoleID, t.UserID, t.Status });

            // Properties
            this.Property(t => t.RoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Status)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("User-Role");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
