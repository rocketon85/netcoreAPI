using Microsoft.EntityFrameworkCore;
using netcoreAPI.Domains;
using netcoreAPI.Helper;
using netcoreAPI.Identity;

namespace netcoreAPI.Context
{
    public class AppDbContext : DbContext
    {
        protected EncryptorHelper? HelperEncryptor;
        public DbSet<FuelDomain> Fuel { get; set; }
        public DbSet<BrandDomain> Brands { get; set; }
        public DbSet<ModelDomain> Models { get; set; }
        public DbSet<CarDomain> Cars { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext()
        {

        }
        public AppDbContext(EncryptorHelper helperEncryptor)
        {
            HelperEncryptor = helperEncryptor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("app");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
              new User() { Id = 1, Name = "Admin", Password = HelperEncryptor?.EncryptString("admin123") },
              new User() { Id = 2, Name = "User", Password = HelperEncryptor?.EncryptString("user123") }
            );
            modelBuilder.Entity<FuelDomain>().HasData(
              new FuelDomain() { Id = 1, Name = "Gasoil" },
              new FuelDomain() { Id = 2, Name = "Electric" }
            );
            modelBuilder.Entity<BrandDomain>().HasData(
                new BrandDomain() { Id = 1, Name = "Honda" },
                new BrandDomain() { Id = 2, Name = "Ford" }
            );
            modelBuilder.Entity<ModelDomain>().HasData(
                new ModelDomain() { Id = 1, BrandId = 1, Name = "Pilot" },
                new ModelDomain() { Id = 2, BrandId = 1, Name = "XR" },
                new ModelDomain() { Id = 3, BrandId = 2, Name = "Mondeo" }
            );
            modelBuilder.Entity<CarDomain>().HasData(
                new CarDomain() { Id = 1, Name = "Auto 1", BrandId = 1, ModelId = 1, FuelId = 1 },
                new CarDomain() { Id = 2, Name = "Auto 2", BrandId = 1, ModelId = 1, FuelId = 2 },
                new CarDomain() { Id = 3, Name = "Auto 3", BrandId = 1, ModelId = 2, FuelId = 2 }
            ); 
        }
    }
}
