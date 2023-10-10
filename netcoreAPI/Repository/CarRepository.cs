using Microsoft.EntityFrameworkCore;
using netcoreAPI.Dal;
using netcoreAPI.Domain;

namespace netcoreAPI.Repository
{
    public class CarRepository : BaseRepository, IRepository<Car>
    {
        public CarRepository(AppDbContext dbContext):base(dbContext) 
        {

        }

        public async Task<IEnumerable<Car>> GetAll()
        {
            return await this.dbContext.Cars.Include(p=> p.Fuel).Include(p => p.Model).Include(p => p.Brand).ToListAsync();
        }

        public async Task<Car?> GetById(int id)
        {
            return await this.dbContext.Cars.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Car?> GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
