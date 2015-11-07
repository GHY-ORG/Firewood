using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataSource.Models.Mapping
{
    public class ActivityMap : EntityTypeConfiguration<Activity>
    {
        public ActivityMap()
        {
            // Primary Key
            this.HasKey(t => t.ActID);

            // Properties
            this.Property(t => t.ActName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ActIntro)
                .IsRequired()
                .HasMaxLength(1000);

            this.Property(t => t.ActPic)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.Class1)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Class2)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Place)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Activity");
            this.Property(t => t.ActID).HasColumnName("ActID");
            this.Property(t => t.ActName).HasColumnName("ActName");
            this.Property(t => t.ActIntro).HasColumnName("ActIntro");
            this.Property(t => t.ActPic).HasColumnName("ActPic");
            this.Property(t => t.OrgID).HasColumnName("OrgID");
            this.Property(t => t.Class1).HasColumnName("Class1");
            this.Property(t => t.Class2).HasColumnName("Class2");
            this.Property(t => t.Place).HasColumnName("Place");
            this.Property(t => t.BeginTime).HasColumnName("BeginTime");
            this.Property(t => t.EndTime).HasColumnName("EndTime");
            this.Property(t => t.MoneyState).HasColumnName("MoneyState");
            this.Property(t => t.ScoreState).HasColumnName("ScoreState");
            this.Property(t => t.AwardState).HasColumnName("AwardState");
            this.Property(t => t.VoteState).HasColumnName("VoteState");
            this.Property(t => t.State).HasColumnName("State");
        }
    }
}
