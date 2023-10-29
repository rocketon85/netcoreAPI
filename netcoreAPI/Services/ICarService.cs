using netcoreAPI.Domains;

namespace netcoreAPI.Services
{
    public interface ICarService
    {
        public Task<CarDomain?> CreateCar(CarDomain car);
    }
}
