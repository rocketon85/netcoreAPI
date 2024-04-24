using netcoreAPI.Context;
using netcoreAPI.Helper;
using netcoreAPI.Services;

namespace netcoreAPI.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly AppDbContext repositoryContext;
        private readonly IEncryptorHelper helperEncryptor;
        private readonly IJwtService jwtService;
        private readonly IAzureFuncService azureFuncService;

        private IUserRepository user;
        private ICarRepository car;

        public IUserRepository User => (user == null ? new UserRepository(repositoryContext, helperEncryptor, azureFuncService, jwtService) : user);
        public ICarRepository Car => (car == null ? new CarRepository(repositoryContext,azureFuncService) : car);
        
        public RepositoryWrapper(AppDbContext repositoryContext, IEncryptorHelper helperEncryptor,
            IAzureFuncService azureFuncService, IJwtService jwtService)
        {
            this.repositoryContext = repositoryContext;
            this.helperEncryptor = helperEncryptor;
            this.azureFuncService = azureFuncService;
            this.jwtService = jwtService;

            this.repositoryContext.Database.EnsureCreated();
        }
        public async Task<int> SaveAsync()
        {
            return await repositoryContext.SaveChangesAsync();
        }

        public int Save()
        {
            return repositoryContext.SaveChanges();
        }
    }
}
