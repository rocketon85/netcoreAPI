using netcoreAPI.Identity;
using netcoreAPI.Models;

namespace netcoreAPI.Services
{
    public interface IUserService
    {
        Task<AuthRespModel?> Authenticate(AuthRequest model);
        Task<User?> GetById(int id);
    }
}
