using netcoreAPI.Context;
using netcoreAPI.Domains;
using netcoreAPI.Options;
using netcoreAPI.Services;
using netcoreAPI.Tests.Collections;

namespace netcoreAPI.Tests.Services
{
    [Collection("Enviroment collection")]
    public class CarServiceTest : BaseService
    {
        private readonly CarService carService;
        public CarServiceTest(StartUp enviroment) : base(enviroment.DbContextContext)
        {
            carService = new CarService(DbContext, new Repositories.CarRepository(DbContext), enviroment.AzureFunctionService);
        }

        [Fact]
        public async void CreateCar()
        {
            CarDomain? resp = await carService.CreateCar(new CarDomain { BrandId = 1, ModelId = 1, Name = "Auto Nuevo" });
            Assert.True(resp?.Id > 0);
        }
    }
}