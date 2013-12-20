using Ninesky.Areas.Admin.Models;
using System.Data.Entity;

namespace Ninesky.Models
{
    public class NineskyContext:DbContext
    {
        public DbSet<SiteConfig> SiteConfig { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<ItemModel> Items { get; set; }
        public DbSet<Article> Articles { get; set; }
        public NineskyContext()
            : base("DefaultConnection")
        {
            Database.CreateIfNotExists();
        }
    }
}