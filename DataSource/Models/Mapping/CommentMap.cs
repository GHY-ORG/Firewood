using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataSource.Models.Mapping
{
    public class CommentMap : EntityTypeConfiguration<Comment>
    {
        public CommentMap()
        {
            // Primary Key
            this.HasKey(t => t.ComID);

            // Properties
            this.Property(t => t.ComCon)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Comment");
            this.Property(t => t.ComID).HasColumnName("ComID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.OrgID).HasColumnName("OrgID");
            this.Property(t => t.ActID).HasColumnName("ActID");
            this.Property(t => t.ComCon).HasColumnName("ComCon");
            this.Property(t => t.ComTime).HasColumnName("ComTime");
            this.Property(t => t.NextComID).HasColumnName("NextComID");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.State).HasColumnName("State");
        }
    }
}
