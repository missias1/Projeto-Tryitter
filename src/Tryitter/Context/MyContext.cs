using Microsoft.EntityFrameworkCore;
using Tryitter.Models;

namespace Tryitter.Context
{
    public class MyContext : DbContext, IMyContext
    {
        public MyContext(DbContextOptions<MyContext>options): base(options)
        {
        }
        public MyContext() { }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=tcp:tryitter-project.database.windows.net,1433;Initial Catalog=tryitter-database;Persist Security Info=False;User ID=admleticia;Password=Senha123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }
    }
}
