using Microsoft.EntityFrameworkCore;
using Moq;
using netcoreAPI.Helper;
using netcoreAPI.Identity;
using netcoreAPI.Options;
using netcoreAPI.Services;
using netcoreAPI.Structures;
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
                var mockAzureService = new Mock<IAzureKeyVaultService>();

                mockAzureService.Setup(x => x.GetSecret(AzureSecrets.EncryptKey)).Returns("E546C8DF278CD5931069B522E695D4F2");

                HelperEncryptor = new EncryptorHelper(Microsoft.Extensions.Options.Options.Create(new SecurityOption { Key = "E546C8DF278CD5931069B522E695D4F2" }), mockAzureService.Object);
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
