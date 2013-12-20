using Ninesky.Models;
using System.Data.Entity;

namespace Ninesky.Repository
{
    public class NineskyContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<CommonModel> CommonModels { get; set; }
        public DbSet<Article> Articles { get; set; }
        public NineskyContext()
            : base("DefaultConnection")
        {
            Database.CreateIfNotExists();
        }
    }
}