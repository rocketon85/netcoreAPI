//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Options;
//using netcoreAPI.Context;
//using netcoreAPI.Options;
//using System.Runtime;

//namespace netcoreAPI.Tests
//{
//    internal class StartUp
//    {
//        private static readonly object _lock = new();
//        public static AppDbContext DbContext { get; private set; }
//        public static IConfiguration Config { get; private set; }
//        public static JwtOption JwtOption { get; private set; }

//        public StartUp()
//        {
//            lock (_lock)
//            {
//                if (DbContext == null)
//                {
//                    DbContext = new TestDbContext();
//                    DbContext.Database.EnsureCreated();
//                }
//                if (Config == null)
//                {
//                    Config = new ConfigurationBuilder()
//                       .AddJsonFile("appsettings.test.json")
//                        .AddEnvironmentVariables()
//                        .Build();
//                }
//                if (JwtOption == null)
//                {
//                    JwtOption = Config.Get<JwtOption>();

//                    //JwtOption = new JwtOption { Audience = "JWTServicePostmanClient", Issuer = "JWTAuthenticationServer", Key = "Yh2k7QSu4l8CZg5p6X3Pna9L0Miy4D3Bvt0JVr87UcOj69Kqw5R2Nmf4FWs03Hdx", Subject = "JWTServiceAccessToken" };
//                }
//            }
//        }
//    }
//}
