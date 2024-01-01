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
        private readonly UserService userService;
        private readonly EncryptorHelper helperEncryptor;
        private readonly JwtOption jwtOption;
        private readonly SecurityOption securityOption;
        public UserServiceTest(StartUp enviroment) : base(enviroment.DbContextContext)
        {
            jwtOption = enviroment.JwtOption;
            securityOption = enviroment.SecurityOption;
            helperEncryptor = new EncryptorHelper(Microsoft.Extensions.Options.Options.Create(securityOption), enviroment.AzureKeyVaultService);
            userService = new UserService(new JwtService(Microsoft.Extensions.Options.Options.Create(jwtOption), enviroment.AzureKeyVaultService), new Repositories.UserRepository(DbContext, helperEncryptor), enviroment.AzureFunctionService);
        }

        [Fact]
        public async void Auth()
        {
            AuthRespModel? resp = await userService.Authenticate(new AuthRequest { Username = "user", Password = "user123" });
            Assert.True(resp?.UserId == 1);
        }
    }
}