using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using netcoreAPI.Contracts.Models.Requests;
using netcoreAPI.Contracts.Models.Responses;
using netcoreAPI.Controllers;
using netcoreAPI.Repositories;
using System.Net;

namespace netcoreAPI.Tests.Controllers
{
    public class UserControllerTest : BaseController
    {
        public UserControllerTest(Collections.StartUp enviroment) : base()
        {
            var mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            mockRepositoryWrapper.Setup<IUserRepository>(x => x.User).Returns(new UserRepository(enviroment.DbContextContext, enviroment.EncryptorHelper, enviroment.AzureFunctionService, enviroment.JwtService));

            Repository = mockRepositoryWrapper.Object;
        }


        [Fact]
        public async Task Authenticate()
        {
            UserController controller = new UserController(null, null, Repository, null);

            var result = await controller.Authenticate(new AuthorizationRequest() { Username = "user", Password = "user123" });
            var actualResult = (ObjectResult)result.Result;
            var actualAttribute = controller.GetType().GetMethod("Authenticate").GetCustomAttributes(typeof(AuthorizeAttribute), true);

            Assert.True(((AuthorizationResponse)actualResult.Value).Token.Trim().Length > 0);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)actualResult.StatusCode);
            Assert.True(!actualAttribute.Any());
        }
    }
}
