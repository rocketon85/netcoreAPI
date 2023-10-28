using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using netcoreAPI.Helper;
using netcoreAPI.Identity;
using netcoreAPI.Options;

namespace netcoreAPI.Dal
{
    public class TestDbContext : AppDbContext, IDisposable
    {
        private DbContextOptionsBuilder optionsBuilder;
        public TestDbContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("test");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if(this.helperEncryptor == null)
            {
                this.helperEncryptor = new EncryptorHelper(Microsoft.Extensions.Options.Options.Create(new ConfigureSecurity { Key = "E546C8DF278CD5931069B522E695D4F2" }));
            }
            modelBuilder.Entity<User>().HasData(
              new User() { Id = 1, Name = "user", Password = this.helperEncryptor.EncryptString("user123") }
            );

        }

        public void Dispose()
        {
            // ... clean up test data from the database ...
        }
    }
}
