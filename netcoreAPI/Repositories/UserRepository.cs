using Microsoft.EntityFrameworkCore;
using netcoreAPI.Context;
using netcoreAPI.Helper;
using netcoreAPI.Identity;
using netcoreAPI.Contracts.Models.Responses;
using netcoreAPI.Contracts.Models.Requests;
using netcoreAPI.Services;
using System.Security.Claims;

namespace netcoreAPI.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly IEncryptorHelper helperEncryptor;
        private readonly IJwtService jwtService;
        private readonly IAzureFuncService azureFuncService;

        public UserRepository(AppDbContext repositoryContext, IEncryptorHelper helperEncryptor,
            IAzureFuncService azureFuncService, IJwtService jwtService) : base(repositoryContext)
        {
            this.helperEncryptor = helperEncryptor;
            this.jwtService = jwtService;
            this.azureFuncService = azureFuncService;
        }

        private async Task<Claim[]> GetClaims(User user)
        {
            if (user.Name.ToLower() == "admin")
            {
                return await Task<Claim[]>.FromResult(new[]
                {
                    new Claim(ClaimTypes.Role, "admin")
                });
            }
            else
            {
                return await Task<Claim[]>.FromResult(new[]
                {
                    new Claim(ClaimTypes.Role, "user")
                });
            }
        }

        //public async Task<User?> Get(string name, string password)
        //{
        //    return await FindByCondition(p => p.Name.ToLower() == name.ToLower() && helperEncryptor.DecryptString(p.Password) == password).SingleOrDefaultAsync();
        //}

        public async Task<AuthorizationResponse?> Authenticate(AuthorizationRequest request)
        {
            var user = await FindByCondition(p => p.Name.ToLower() == request.Username.ToLower() && helperEncryptor.DecryptString(p.Password) == request.Password).SingleOrDefaultAsync();

            // return null if user not found
            if (user == null) return null;
            //trigger the Azure Function to retrieve specific user data.
            user.Detail = await azureFuncService.FuncGetUserDetail(user.Id);
            // authentication successful so generate jwt token
            var token = jwtService.GenerateJwtToken(user, await GetClaims(user));

            return await Task<AuthorizationResponse>.FromResult(new AuthorizationResponse() { Token= token, UserId= user.Id });
        }
    }
}
