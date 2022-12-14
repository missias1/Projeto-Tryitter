using Microsoft.EntityFrameworkCore;
using Tryitter.Models;

namespace Tryitter.Context
{
    public interface IMyContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public int SaveChanges();
    }
}
