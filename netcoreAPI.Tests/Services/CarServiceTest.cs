using netcoreAPI.Context;
using netcoreAPI.Domains;
using netcoreAPI.Services;

namespace netcoreAPI.Tests.Services
{
    [Collection("Database collection")]
    public class CarServiceTest : BaseService
    {
        private readonly CarService _carService;
        public CarServiceTest(TestDbContext dbContext) : base(dbContext)
        {
            _carService = new CarService(DbContext, new Repositories.CarRepository(DbContext));
        }

        [Fact]
        public async void CreateCar()
        {
            CarDomain? resp = await _carService.CreateCar(new CarDomain { BrandId = 1, ModelId = 1, Name = "Auto Nuevo" });
            Assert.True(resp?.Id > 0);
        }
    }
}