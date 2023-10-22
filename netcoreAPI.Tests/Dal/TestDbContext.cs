using Microsoft.EntityFrameworkCore;
using netcoreAPI.Identity;

namespace netcoreAPI.Dal
{
    public class TestDbContext : AppDbContext, IDisposable
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("test");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
              new User() { Id = 1, Name = "user", Password = "user" }
            );

        }

        public void Dispose()
        {
            // ... clean up test data from the database ...
        }
    }
}
