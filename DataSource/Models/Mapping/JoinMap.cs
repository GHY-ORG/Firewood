using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataSource.Models.Mapping
{
    public class JoinMap : EntityTypeConfiguration<Join>
    {
        public JoinMap()
        {
            // Primary Key
            this.HasKey(t => new { t.UserID, t.ActID, t.Status });

            // Properties
            this.Property(t => t.Status)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Join");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.ActID).HasColumnName("ActID");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
