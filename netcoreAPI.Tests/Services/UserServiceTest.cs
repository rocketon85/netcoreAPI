using netcoreAPI.Context;
using netcoreAPI.Helper;
using netcoreAPI.Models;
using netcoreAPI.Options;
using netcoreAPI.Services;
using netcoreAPI.Tests.Collections;

namespace netcoreAPI.Tests.Services
{
    [Collection("Enviroment collection")]
    public class UserServiceTest : BaseService
    {
        private readonly UserService _userService;
        private readonly EncryptorHelper _helperEncryptor;
        private readonly JwtOption _jwtOption;
        private readonly SecurityOption _securityOption;
        public UserServiceTest(StartUp enviroment) : base(enviroment.DbContextContext)
        {
            _jwtOption = enviroment.JwtOption;
            _securityOption = enviroment.SecurityOption;
            _helperEncryptor = new EncryptorHelper(Microsoft.Extensions.Options.Options.Create(_securityOption), enviroment.AzureKeyVaultService);
            _userService = new UserService(new JwtService(Microsoft.Extensions.Options.Options.Create(_jwtOption), enviroment.AzureKeyVaultService), new Repositories.UserRepository(DbContext, _helperEncryptor));
        }

        [Fact]
        public async void Auth()
        {
            AuthRespModel? resp = await _userService.Authenticate(new AuthRequest { Username = "user", Password = "user123" });
            Assert.True(resp?.UserId == 1);
        }
    }
}