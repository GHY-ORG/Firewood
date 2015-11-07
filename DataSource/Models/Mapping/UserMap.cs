using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataSource.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.UserID);

            // Properties
            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.NickName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.StuNumber)
                .HasMaxLength(50);

            this.Property(t => t.Avatar)
                .HasMaxLength(255);

            this.Property(t => t.Tel)
                .HasMaxLength(50);

            this.Property(t => t.TrueName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("User");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.NickName).HasColumnName("NickName");
            this.Property(t => t.StuNumber).HasColumnName("StuNumber");
            this.Property(t => t.Avatar).HasColumnName("Avatar");
            this.Property(t => t.Sex).HasColumnName("Sex");
            this.Property(t => t.Tel).HasColumnName("Tel");
            this.Property(t => t.TrueName).HasColumnName("TrueName");
            this.Property(t => t.RegisterTime).HasColumnName("RegisterTime");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
