using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataSource.Models.Mapping
{
    public class OrgMap : EntityTypeConfiguration<Org>
    {
        public OrgMap()
        {
            // Primary Key
            this.HasKey(t => t.OrgID);

            // Properties
            this.Property(t => t.OrgName)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.OrgPic)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.OrgDepartment)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.OrgIntroduction)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.OrgContact)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Org");
            this.Property(t => t.OrgID).HasColumnName("OrgID");
            this.Property(t => t.OrgName).HasColumnName("OrgName");
            this.Property(t => t.OrgPic).HasColumnName("OrgPic");
            this.Property(t => t.OrgDepartment).HasColumnName("OrgDepartment");
            this.Property(t => t.OrgIntroduction).HasColumnName("OrgIntroduction");
            this.Property(t => t.OrgContact).HasColumnName("OrgContact");
            this.Property(t => t.RegisterTime).HasColumnName("RegisterTime");
            this.Property(t => t.LastLogin).HasColumnName("LastLogin");
            this.Property(t => t.State).HasColumnName("State");
        }
    }
}
