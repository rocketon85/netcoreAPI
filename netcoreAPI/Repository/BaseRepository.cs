using netcoreAPI.Dal;

namespace netcoreAPI.Repository
{
    public class BaseRepository
    {
        protected readonly AppDbContext dbContext;

        public BaseRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbContext.Database.EnsureCreated();
        }

     
    }
}
