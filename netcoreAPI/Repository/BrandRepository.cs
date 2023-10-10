using Microsoft.EntityFrameworkCore;
using netcoreAPI.Dal;
using netcoreAPI.Domain;

namespace netcoreAPI.Repository
{
    public class BrandRepository : BaseRepository, IRepository<Brand>
    {
        public BrandRepository(AppDbContext dbContext):base(dbContext) 
        {

        }

        public async Task<IEnumerable<Brand>> GetAll()
        {
            return await this.dbContext.Brands.ToListAsync();
        }

        public async Task<Brand?> GetById(int id)
        {
            return await this.dbContext.Brands.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Brand?> GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
