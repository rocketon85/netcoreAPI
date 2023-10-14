using netcoreAPI.Domain;
using netcoreAPI.Identity;
using netcoreAPI.Models;

namespace netcoreAPI.Services
{
    public interface ICarService
    {
        public Task<int?> CreateCar(Car car);
    }
}
