using Microsoft.EntityFrameworkCore;
using netcoreAPI.Helper;
using netcoreAPI.Identity;
using netcoreAPI.Options;

namespace netcoreAPI.Context
{
    public class TestDbContext : AppDbContext, IDisposable
    {
        public TestDbContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("test");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (HelperEncryptor == null)
            {
                HelperEncryptor = new EncryptorHelper(Microsoft.Extensions.Options.Options.Create(new SecurityOption { Key = "E546C8DF278CD5931069B522E695D4F2" }));
            }
            modelBuilder.Entity<User>().HasData(
              new User() { Id = 1, Name = "user", Password = HelperEncryptor.EncryptString("user123") }
            );
        }

        public void Dispose()
        {

        }
    }
}
