using Microsoft.EntityFrameworkCore;
using netcoreAPI.Dal;
using netcoreAPI.Domain;

namespace netcoreAPI.Repository
{
    public class CarRepository : BaseRepository, IRepository<Car>
    {
        public CarRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Car>> GetAll()
        {
            return await this.dbContext.Cars.Include(p => p.Fuel)
                .Include(p => p.Model)
                .Include(p => p.Brand)
                .Include(p => p.Fuel)
                .Select(p => new Car
                {
                    Id = p.Id,
                    Name = p.Name,
                })
                .ToListAsync();
        }

        public IQueryable<Car> GetAllQueryable()
        {
            return this.dbContext.Cars.Include(p => p.Fuel)
                .Include(p => p.Model)
                .Include(p => p.Brand)
                .Include(p => p.Fuel)
                .Select(p => new Car
                {
                    Id = p.Id,
                    Name = p.Name,
                });
        }

        public async Task<Car?> GetById(int id)
        {
            return await this.dbContext.Cars.Include(p => p.Model).Include(p => p.Brand).Include(p => p.Fuel).SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Car?> GetByName(string name)
        {
            return await this.dbContext.Cars.SingleOrDefaultAsync(p => p.Name.ToLowerInvariant() == name.ToLowerInvariant());
        }
    }
}
