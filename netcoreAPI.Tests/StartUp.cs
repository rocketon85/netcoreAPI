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
    internal class StartUp
    {
        private static readonly object _lock = new();
        public static AppDbContext DbContext { get; private set; }

        public static JwtSettings JwtSettings { get; private set; } 

        public StartUp()
        {
            lock (_lock)
            {
                if (DbContext == null)
                {
                    DbContext = new TestDbContext();
                    DbContext.Database.EnsureCreated();
                }
                if (JwtSettings == null)
                {
                    JwtSettings = new JwtSettings { Audience = "JWTServicePostmanClient", Issuer = "JWTAuthenticationServer", Key = "Yh2k7QSu4l8CZg5p6X3Pna9L0Miy4D3Bvt0JVr87UcOj69Kqw5R2Nmf4FWs03Hdx", Subject = "JWTServiceAccessToken" };
                }
            }
        }
    }
}
