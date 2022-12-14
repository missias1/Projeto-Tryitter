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
                optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=tryitter;Trusted_Connection=True;Encrypt=False;");
            }
        }
    }
}
