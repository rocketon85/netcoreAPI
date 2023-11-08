﻿using Microsoft.EntityFrameworkCore;
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
        public StartUp()
        {
            DbContextContext = new TestDbContext();

            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.test.json")
                .AddEnvironmentVariables()
                .Build();

            JwtOption = config.GetSection("JwtSettings").Get<JwtOption>();
            SecurityOption = config.GetSection("SecuritySettings").Get<SecurityOption>();
            
            var mockAzureService = new Mock<IAzureKeyVaultService>();

            mockAzureService.Setup<string>(x => x.GetSecret(AzureSecrets.JWTKey)).Returns(JwtOption.Key);
            mockAzureService.Setup(x => x.GetSecret(AzureSecrets.EncryptKey)).Returns(SecurityOption.Key);

            AzureKeyVaultService = mockAzureService.Object;
        }
    }
}
