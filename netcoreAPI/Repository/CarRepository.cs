using netcoreAPI.Dal;
using netcoreAPI.Domain;

namespace netcoreAPI.Repository
{
    public class CarRepository : BaseRepository, IRepository<Car>
    {
        public CarRepository(AppDbContext dbContext):base(dbContext) 
        {

        }

        public IEnumerable<Car> GetAll()
        {
            return this.dbContext.Cars.ToList();
        }

        public Car GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Car GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
