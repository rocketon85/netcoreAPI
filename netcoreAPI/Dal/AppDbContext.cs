using Microsoft.EntityFrameworkCore;
using netcoreAPI.Domain;
using netcoreAPI.Identity;

namespace netcoreAPI.Dal
{
    public class AppDbContext : DbContext
    {
        public DbSet<Fuel> Fuel { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("app");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
              new User() { Id = 1, Name = "Admin", Password="admin123" },
              new User() { Id = 2, Name = "User" , Password="user123"}
            );

            modelBuilder.Entity<Fuel>().HasData(
              new Fuel() { Id = 1, Name = "Gasoil" },
              new Fuel() { Id = 2, Name = "Electric" }
            );

            modelBuilder.Entity<Brand>().HasData(
                new Brand() { Id = 1, Name = "Honda" },
                new Brand() { Id = 2, Name = "Ford" }
            );

            modelBuilder.Entity<Model>().HasData(
                new Model() { Id = 1, BrandId = 1, Name = "Pilot" },
                new Model() { Id = 2, BrandId = 1, Name = "XR" },
                new Model() { Id = 3, BrandId = 2, Name = "Mondeo" }
            );

            modelBuilder.Entity<Car>().HasData(
                new Car() { Id = 1, Name= "Auto 1", BrandId = 1, ModelId = 1, FuelId = 1 },
                new Car() { Id = 2, Name = "Auto 2", BrandId = 1, ModelId = 1, FuelId = 2 },
                new Car() { Id = 3, Name = "Auto 3", BrandId = 1, ModelId = 2, FuelId = 2 }
            );

           // modelBuilder.Entity("BrandModel").HasData(
           //     new[]
           //     {
           //         new { BranModelId = 1, BrandId = 1, ModelId = 1 },
           //         new { BranModelId = 2, BrandId = 1, ModelId = 2 },
           //         new { BranModelId = 3, BrandId = 2, ModelId = 3 },
           //     }
           //);
        }
    }
}
