using netcoreAPI.Domain;
using netcoreAPI.Identity;
using Microsoft.EntityFrameworkCore;

namespace netcoreAPI.Dal
{
    public class TestDbContext : AppDbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("test");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
              new User() { Id = 1, Name = "user", Password="user" }
            );

        }
    }
}
