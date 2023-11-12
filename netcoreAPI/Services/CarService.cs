using netcoreAPI.Context;
using netcoreAPI.Domains;
using netcoreAPI.Repositories;

namespace netcoreAPI.Services
{
    public class CarService : ICarService
    {
        private readonly CarRepository _carRepository;
        private readonly AppDbContext _dbContext;
        private readonly IAzureFuncService _azureFuncService;

        public CarService(AppDbContext dbContext, CarRepository carRepository, IAzureFuncService azureFuncService)
        {
            _dbContext = dbContext;
            _carRepository = carRepository;
            _azureFuncService = azureFuncService;
        }

        public async Task<CarDomain?> CreateCar(CarDomain model)
        {
            _dbContext.Add<CarDomain>(model);
            int result = await _dbContext.SaveChangesAsync();
            //model = await carRepository.GetById(model.Id);
            var car = _dbContext.Cars.SingleOrDefault(p => p.Id == model.Id);
            if (result == 1) _azureFuncService.FuncNewCar(car);
            return model;
        }
    }
}
