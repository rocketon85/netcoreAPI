using Microsoft.EntityFrameworkCore;
using netcoreAPI.Context;
using netcoreAPI.Domains;

namespace netcoreAPI.Repositories
{
    public class BrandRepository : RepositoryBase<BrandDomain>, IBrandRepository
    {
        public BrandRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

    }
}
