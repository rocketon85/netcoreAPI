using Microsoft.EntityFrameworkCore;
using netcoreAPI.Context;
using netcoreAPI.Helper;
using netcoreAPI.Identity;
using netcoreAPI.Models;

namespace netcoreAPI.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        public Task<AuthRespModel?> Authenticate(AuthRequest request);
    }
}
