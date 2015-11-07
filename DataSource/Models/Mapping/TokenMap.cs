using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataSource.Models.Mapping
{
    public class TokenMap : EntityTypeConfiguration<Token>
    {
        public TokenMap()
        {
            // Primary Key
            this.HasKey(t => t.TokenID);

            // Properties
            this.Property(t => t.TokenCode)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.CheckCode)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Token");
            this.Property(t => t.TokenID).HasColumnName("TokenID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.TokenCode).HasColumnName("TokenCode");
            this.Property(t => t.CheckCode).HasColumnName("CheckCode");
            this.Property(t => t.Expire).HasColumnName("Expire");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
