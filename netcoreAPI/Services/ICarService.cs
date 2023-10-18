using netcoreAPI.Domain;

namespace netcoreAPI.Services
{
    public interface ICarService
    {
        public Task<Car?> CreateCar(Car car);
    }
}
