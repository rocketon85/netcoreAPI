using netcoreAPI.Domains;
using netcoreAPI.Tests.Collections;

namespace netcoreAPI.Tests.Repositories
{
    [Collection("Enviroment collection")]
    public class CarRepositoryTest : BaseRepository
    {
        public CarRepositoryTest(StartUp enviroment) : base(enviroment.DbContextContext, enviroment.EncryptorHelper, enviroment.AzureFunctionService, enviroment.JwtService)
        {

        }

        [Fact]
        public async void CreateCar()
        {
            CarDomain? resp = await Repository.Car.CreateAsync(new CarDomain { BrandId = 1, ModelId = 1, Name = "Auto Nuevo" });
            Assert.True(resp?.Id > 0);
        }
    }
}