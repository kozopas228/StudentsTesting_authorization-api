using Authorization_Models;
using Microsoft.EntityFrameworkCore;

namespace Authorization_Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TestAttempt> TestAttempts { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}