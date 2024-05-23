using netcoreAPI.Contracts.Models.Requests;
using netcoreAPI.Contracts.Models.Responses;
using netcoreAPI.Identity;

namespace netcoreAPI.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        public Task<AuthorizationResponse?> Authenticate(AuthorizationRequest request);
    }
}
