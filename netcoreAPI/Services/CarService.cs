using netcoreAPI.Dal;
using netcoreAPI.Domain;
using netcoreAPI.Repository;

namespace netcoreAPI.Services
{
    public class CarService: ICarService
    {
        private readonly CarRepository carRepository;
        private readonly AppDbContext dbContext;

        public CarService(AppDbContext dbContext, CarRepository carRepository)
        {
            this.dbContext = dbContext;
            this.carRepository = carRepository;
        }

        public async Task<Car?> CreateCar(Car model)
        {
            this.dbContext.Add<Car>(model);
            int result = await this.dbContext.SaveChangesAsync();
            //model = await carRepository.GetById(model.Id);
            var car = this.dbContext.Cars.SingleOrDefault(p=> p.Id == model.Id);
            return model;
        }
    }
}
