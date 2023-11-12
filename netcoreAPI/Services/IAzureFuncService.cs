using netcoreAPI.Domains;
using netcoreAPI.Identity;
using System.Security.Claims;

namespace netcoreAPI.Services
{
    public interface IAzureFuncService
    {
        public Task<string> FuncNewCar(CarDomain data);

        public Task<string> FuncGetUserDetail(int userId);
    }
}
