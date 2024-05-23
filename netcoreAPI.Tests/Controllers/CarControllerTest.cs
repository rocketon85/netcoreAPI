using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using netcoreAPI.Controllers.V1;
using netcoreAPI.Contracts.Models.Requests.V1;
using netcoreAPI.Contracts.Models.Responses.V1;
using netcoreAPI.Repositories;
using System.Net;

namespace netcoreAPI.Tests.Controllers
{
    [Collection("Enviroment collection")]
    public class CarControllerTest : BaseController
    {

        public CarControllerTest(Collections.StartUp enviroment) : base()
        {
            var mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            mockRepositoryWrapper.Setup<ICarRepository>(x => x.Car).Returns(new CarRepository(enviroment.DbContextContext, enviroment.AzureFunctionService));

            Repository = mockRepositoryWrapper.Object;
        }


        [Fact]
        public async Task CreateCar()
        {
            CarController controller = new CarController(null, Repository, Mapper);

            var result = await controller.CreateCar(new CarCreateRequest() { BrandId = 1, FuelId = 1, ModelId = 1 });
            var actualResult = (ObjectResult)result.Result;
            var actualAttribute = controller.GetType().GetMethod("CreateCar").GetCustomAttributes(typeof(AuthorizeAttribute), true);

            Assert.True(((CarViewResponse)actualResult.Value).Id > 0);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)actualResult.StatusCode);
            Assert.Equal(typeof(AuthorizeAttribute), actualAttribute[0].GetType());
            Assert.True( ((Microsoft.AspNetCore.Authorization.AuthorizeAttribute)actualAttribute[0]).Roles.Contains("admin"));
        }
    }
}
