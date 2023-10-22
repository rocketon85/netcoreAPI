using Microsoft.Extensions.Options;
using netcoreAPI.Helper;
using netcoreAPI.Models;
using netcoreAPI.Services;

namespace netcoreAPI.Tests.Services
{
    public class CarServiceTest : BaseService
    {
        private readonly CarService service;
        public CarServiceTest() : base()
        {
            service = new CarService(StartUp.DbContext, new Repository.CarRepository(StartUp.DbContext));
        }

        [Fact]
        public async void CreateCar()
        {
            Domain.Car? resp = await service.CreateCar(new Domain.Car { BrandId = 1, ModelId = 1, Name = "Auto Nuevo" });
            Assert.True(resp?.Id > 0);
        }
    }
}