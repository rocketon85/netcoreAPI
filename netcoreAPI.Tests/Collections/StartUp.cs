using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using netcoreAPI.Context;
using netcoreAPI.Options;
using netcoreAPI.Services;
using netcoreAPI.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
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

            mockAzureFunction.Setup<Task<string>>(x => x.FuncGetUserDetail(0)).Returns(Task<string>.FromResult("user detail"));
            mockAzureFunction.Setup<Task<string>>(x => x.FuncNewCar(new Domains.CarDomain())).Returns(Task<string>.FromResult("new car added"));

            AzureFunctionService = mockAzureFunction.Object;
        }
    }
}
