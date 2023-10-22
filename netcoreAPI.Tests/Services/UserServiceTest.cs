using Microsoft.Extensions.Options;
using netcoreAPI.Helper;
using netcoreAPI.Models;
using netcoreAPI.Services;

namespace netcoreAPI.Tests.Services
{
    public class UserServiceTest : BaseService
    {
        private readonly UserService service;
        public UserServiceTest() : base()
        {
            service = new UserService(new JwtService(Microsoft.Extensions.Options.Options.Create(StartUp.JwtSettings)), new Repository.UserRepository(StartUp.DbContext));
        }

        [Fact]
        public async void Auth()
        {
            AuthRespModel? resp = await service.Authenticate(new AuthRequest { Username = "user", Password = "user" });
            Assert.True(resp?.UserId == 1);
        }
    }
}