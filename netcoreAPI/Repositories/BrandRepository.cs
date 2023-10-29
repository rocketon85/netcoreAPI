using Microsoft.EntityFrameworkCore;
using netcoreAPI.Context;
using netcoreAPI.Domains;

namespace netcoreAPI.Repositories
{
    public class BrandRepository : BaseRepository, IRepository<BrandDomain>
    {
        public BrandRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<BrandDomain>> GetAll()
        {
            return await DbContext.Brands.ToListAsync();
        }

        public async Task<BrandDomain?> GetById(int id)
        {
            return await DbContext.Brands.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<BrandDomain?> GetByName(string name)
        {
            return await DbContext.Brands.SingleOrDefaultAsync(p => p.Name.ToLowerInvariant() == name.ToLowerInvariant());
        }
    }
}
