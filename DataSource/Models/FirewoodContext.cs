using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DataSource.Models.Mapping;

namespace DataSource.Models
{
    public partial class FirewoodContext : DbContext
    {
        static FirewoodContext()
        {
            Database.SetInitializer<FirewoodContext>(null);
        }

        public FirewoodContext()
            : base("Name=FirewoodContext")
        {
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Join> Joins { get; set; }
        public DbSet<Org> Orgs { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ActivityMap());
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new JoinMap());
            modelBuilder.Configurations.Add(new OrgMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
        }
    }
}
