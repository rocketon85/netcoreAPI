using netcoreAPI.Dal;
using netcoreAPI.Domain;

namespace netcoreAPI.Repository
{
    public class BrandRepository : BaseRepository, IRepository<Brand>
    {
        public BrandRepository(AppDbContext dbContext):base(dbContext) 
        {

        }

        public IEnumerable<Brand> GetAll()
        {
            return this.dbContext.Brands.ToList();
        }

        public Brand GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Brand GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
