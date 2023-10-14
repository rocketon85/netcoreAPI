using Microsoft.AspNetCore.Http.HttpResults;
using netcoreAPI.Dal;
using netcoreAPI.Domain;
using netcoreAPI.Hubs;
using netcoreAPI.Identity;
using netcoreAPI.Models;
using netcoreAPI.Repository;

namespace netcoreAPI.Services
{
    public class CarService: ICarService
    {
        private readonly CarRepository carRepository;
        private readonly AppDbContext dbContext;

        public CarService(AppDbContext dbContext, CarRepository carRepository)
        {
            this.dbContext = dbContext;
            this.carRepository = carRepository;
        }

        public async Task<int?> CreateCar(Car model)
        {
            this.dbContext.Add<Car>(model);
            return await this.dbContext.SaveChangesAsync();
        }
    }
}
