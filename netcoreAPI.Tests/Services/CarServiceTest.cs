using Microsoft.Extensions.Options;
using netcoreAPI.Dal;
using netcoreAPI.Helper;
using netcoreAPI.Models;
using netcoreAPI.Services;

namespace netcoreAPI.Tests.Services
{
    [Collection("Database collection")]
    public class CarServiceTest : BaseService
    {
        private readonly CarService service;
        public CarServiceTest(TestDbContext dbContext) : base(dbContext)
        {
            service = new CarService(this.dbContext, new Repository.CarRepository(this.dbContext));
        }

        [Fact]
        public async void CreateCar()
        {
            Domain.Car? resp = await service.CreateCar(new Domain.Car { BrandId = 1, ModelId = 1, Name = "Auto Nuevo" });
            Assert.True(resp?.Id > 0);
        }
    }
}