using netcoreAPI.Context;
using netcoreAPI.Helper;
using netcoreAPI.Repositories;
using netcoreAPI.Services;

namespace netcoreAPI.Tests.Repositories
{
    [Collection("Enviroment collection")]
    public class BaseRepository
    {
        protected readonly AppDbContext DbContext;
        protected readonly IRepositoryWrapper Repository;

        public BaseRepository(TestDbContext dbContext, IEncryptorHelper encryptorHelper , IAzureFuncService azureFunctionService, IJwtService jwtService)
        {
            DbContext = dbContext;
            Repository = new RepositoryWrapper(DbContext, encryptorHelper, azureFunctionService, jwtService);
        }
    }
}
