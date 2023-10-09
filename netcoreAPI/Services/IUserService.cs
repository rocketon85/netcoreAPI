using netcoreAPI.Identity;
using netcoreAPI.Models;

namespace netcoreAPI.Services
{
    public interface IUserService
    {
        dynamic? Authenticate(AuthRequest model);
        User? GetById(int id);
    }
}
