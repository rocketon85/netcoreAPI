using netcoreAPI.Contracts.Models.Requests;
using netcoreAPI.Contracts.Models.Responses;
using netcoreAPI.Tests.Collections;

namespace netcoreAPI.Tests.Repositories
{
    [Collection("Enviroment collection")]
    public class UserRepositoryTest : BaseRepository
    { 
        public UserRepositoryTest(StartUp enviroment) : base(enviroment.DbContextContext, enviroment.EncryptorHelper, enviroment.AzureFunctionService, enviroment.JwtService)
        {

        }

        [Fact]
        public async void Authenticate()
        {
            AuthorizationResponse? resp = await Repository.User.Authenticate(new AuthorizationRequest { Username = "user", Password = "user123" });
            Assert.True(resp?.UserId == 1);
        }
    }
}