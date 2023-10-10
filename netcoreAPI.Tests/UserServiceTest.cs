using Microsoft.Extensions.Options;
using netcoreAPI.Helper;
using netcoreAPI.Models;
using netcoreAPI.Services;

namespace netcoreAPI.Tests
{
    public class UserServiceTest:BaseTest
    {
        private readonly UserService service;
        public UserServiceTest() :base() {
            service = new UserService(new JwtService(Options.Create( StartUp.jwtSettings)), new Repository.UserRepository(StartUp.dbContext));
        }

        [Fact]
        public async void Auth()
        {
            AuthRespModel resp = await this.service.Authenticate(new Models.AuthRequest { Username = "user", Password = "user" });
            Assert.True(resp.UserId == 1);
        }
    }
}