using netcoreAPI.Context;
using netcoreAPI.Helper;
using netcoreAPI.Models;
using netcoreAPI.Options;
using netcoreAPI.Services;

namespace netcoreAPI.Tests.Services
{
    [Collection("Database collection")]
    public class UserServiceTest : BaseService
    {
        private readonly UserService _userService;
        private readonly EncryptorHelper _helperEncryptor;
        private readonly JwtOption _jwtOption = new JwtOption { Audience = "JWTServicePostmanClient", Issuer = "JWTAuthenticationServer", Key = "Yh2k7QSu4l8CZg5p6X3Pna9L0Miy4D3Bvt0JVr87UcOj69Kqw5R2Nmf4FWs03Hdx", Subject = "JWTServiceAccessToken" };
        private readonly SecurityOption _securityOption = new SecurityOption { Key = "E546C8DF278CD5931069B522E695D4F2" };
        public UserServiceTest(TestDbContext dbContext) : base(dbContext)
        {
            _helperEncryptor = new EncryptorHelper(Microsoft.Extensions.Options.Options.Create(_securityOption));
            _userService = new UserService(new JwtService(Microsoft.Extensions.Options.Options.Create(_jwtOption)), new Repositories.UserRepository(DbContext, _helperEncryptor));
        }

        [Fact]
        public async void Auth()
        {
            AuthRespModel? resp = await _userService.Authenticate(new AuthRequest { Username = "user", Password = "user123" });
            Assert.True(resp?.UserId == 1);
        }
    }
}