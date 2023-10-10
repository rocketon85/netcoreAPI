using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using netcoreAPI.Dal;
using Moq;
using netcoreAPI.Identity;
using System.Xml;
using Moq.EntityFrameworkCore;
using netcoreAPI.Helper;

namespace netcoreAPI.Tests
{
    internal static class StartUp
    {

        public static AppDbContext dbContext { get; } = new TestDbContext();

        public static JwtSettings jwtSettings { get; } = new JwtSettings { Audience = "JWTServicePostmanClient", Issuer = "JWTAuthenticationServer", Key = "Yh2k7QSu4l8CZg5p6X3Pna9L0Miy4D3Bvt0JVr87UcOj69Kqw5R2Nmf4FWs03Hdx", Subject = "JWTServiceAccessToken" };

        public static void SetUp()
        {
           
        }
    }
}
