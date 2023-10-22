using Microsoft.Extensions.Options;
using netcoreAPI.Dal;
using netcoreAPI.Helper;
using netcoreAPI.Models;
using netcoreAPI.Services;

namespace netcoreAPI.Tests.Services
{
    [Collection("Database collection")]
    public class UserServiceTest : BaseService
    {
        private readonly UserService service;
        private readonly JwtSettings jwtSettings = new JwtSettings { Audience = "JWTServicePostmanClient", Issuer = "JWTAuthenticationServer", Key = "Yh2k7QSu4l8CZg5p6X3Pna9L0Miy4D3Bvt0JVr87UcOj69Kqw5R2Nmf4FWs03Hdx", Subject = "JWTServiceAccessToken" };
    public UserServiceTest(TestDbContext dbContext) : base(dbContext)
        {
            service = new UserService(new JwtService(Microsoft.Extensions.Options.Options.Create(this.jwtSettings)), new Repository.UserRepository(this.dbContext));
        }

        [Fact]
        public async void Auth()
        {
            AuthRespModel? resp = await service.Authenticate(new AuthRequest { Username = "user", Password = "user" });
            Assert.True(resp?.UserId == 1);
        }
    }
}