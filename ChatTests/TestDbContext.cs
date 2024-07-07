using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatTests
{ 
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Chat> Chats { get; set; }
    }
}
