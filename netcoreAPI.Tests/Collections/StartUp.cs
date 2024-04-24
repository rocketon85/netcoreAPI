using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using netcoreAPI.Context;
using netcoreAPI.Helper;
using netcoreAPI.Identity;
using netcoreAPI.Options;
using netcoreAPI.Services;
using netcoreAPI.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace netcoreAPI.Tests.Collections
{
    public class StartUp
    {
        public TestDbContext DbContextContext { get; set; }
        public JwtOption JwtOption { get; private set; }
        public SecurityOption SecurityOption { get; private set; }
        public IAzureKeyVaultService AzureKeyVaultService { get; set; }
        public IAzureFuncService AzureFunctionService { get; set; }
        public IEncryptorHelper EncryptorHelper { get; set; }
        public IJwtService JwtService { get; set; } 

        public StartUp()
        {
            DbContextContext = new TestDbContext();

            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.test.json")
                .AddEnvironmentVariables()
                .Build();

            JwtOption = config.GetSection("JwtSettings").Get<JwtOption>();
            SecurityOption = config.GetSection("SecuritySettings").Get<SecurityOption>();
            
            var mockAzureKeyVault = new Mock<IAzureKeyVaultService>();

            mockAzureKeyVault.Setup<string>(x => x.GetSecret(AzureSecrets.JWTKey)).Returns(JwtOption.Key);
            mockAzureKeyVault.Setup(x => x.GetSecret(AzureSecrets.EncryptKey)).Returns(SecurityOption.Key);

            AzureKeyVaultService = mockAzureKeyVault.Object;

            var mockAzureFunction = new Mock<IAzureFuncService>();

            mockAzureFunction.Setup<Task<string>>(x => x.FuncGetUserDetail(It.IsAny<int>())).Returns(Task<string>.FromResult("user detail"));
            mockAzureFunction.Setup<Task<string>>(x => x.FuncNewCar(new Domains.CarDomain())).Returns(Task<string>.FromResult("new car added"));

            AzureFunctionService = mockAzureFunction.Object;

            var mockEncrypterHelper = new Mock<IEncryptorHelper>();

            mockEncrypterHelper.Setup<string>(x => x.EncryptString(It.IsAny<string>())).Returns<string>(x => x);
            mockEncrypterHelper.Setup<string>(x => x.DecryptString(It.IsAny<string>())).Returns<string>(x => x);

            EncryptorHelper = mockEncrypterHelper.Object;

            var mockJwtService = new Mock<IJwtService>();

            mockJwtService.Setup<string>(x => x.GenerateJwtToken(It.IsAny<User>(), It.IsAny<Claim[]>())).Returns("token");
            mockJwtService.Setup<int?>(x => x.ValidateJwtToken(It.IsAny<string>())).Returns(1);

            JwtService = mockJwtService.Object;
        }
    }
}
