using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DataSource.Models.Mapping;

namespace DataSource.Models
{
    public partial class ExtensionFrameworkContext : DbContext
    {
        static ExtensionFrameworkContext()
        {
            Database.SetInitializer<ExtensionFrameworkContext>(null);
        }

        public ExtensionFrameworkContext()
            : base("Name=ExtensionFrameworkContext")
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<User_Role> User_Role { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new TokenMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new User_RoleMap());
        }
    }
}
