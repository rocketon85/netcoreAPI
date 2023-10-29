using netcoreAPI.Context;
using netcoreAPI.Domains;
using netcoreAPI.Repositories;

namespace netcoreAPI.Services
{
    public class CarService : ICarService
    {
        private readonly CarRepository _carRepository;
        private readonly AppDbContext _dbContext;

        public CarService(AppDbContext dbContext, CarRepository carRepository)
        {
            _dbContext = dbContext;
            _carRepository = carRepository;
        }

        public async Task<CarDomain?> CreateCar(CarDomain model)
        {
            _dbContext.Add<CarDomain>(model);
            int result = await _dbContext.SaveChangesAsync();
            //model = await carRepository.GetById(model.Id);
            var car = _dbContext.Cars.SingleOrDefault(p => p.Id == model.Id);
            return model;
        }
    }
}
