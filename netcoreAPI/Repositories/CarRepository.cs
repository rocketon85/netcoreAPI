using Microsoft.EntityFrameworkCore;
using netcoreAPI.Context;
using netcoreAPI.Domains;

namespace netcoreAPI.Repositories
{
    public class CarRepository : BaseRepository, IRepository<CarDomain>
    {
        public CarRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<CarDomain>> GetAll()
        {
            return await DbContext.Cars.Include(p => p.Fuel)
                .Include(p => p.Model)
                .Include(p => p.Brand)
                .Include(p => p.Fuel)
                .Select(p => new CarDomain
                {
                    Id = p.Id,
                    Name = p.Name,
                })
                .ToListAsync();
        }

        public IQueryable<CarDomain> GetAllQueryable()
        {
            return DbContext.Cars.Include(p => p.Fuel)
                .Include(p => p.Model)
                .Include(p => p.Brand)
                .Include(p => p.Fuel)
                .Select(p => new CarDomain
                {
                    Id = p.Id,
                    Name = p.Name,
                });
        }

        public async Task<CarDomain?> GetById(int id)
        {
            return await DbContext.Cars.Include(p => p.Model).Include(p => p.Brand).Include(p => p.Fuel).SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<CarDomain?> GetByName(string name)
        {
            return await DbContext.Cars.SingleOrDefaultAsync(p => p.Name.ToLowerInvariant() == name.ToLowerInvariant());
        }
    }
}
