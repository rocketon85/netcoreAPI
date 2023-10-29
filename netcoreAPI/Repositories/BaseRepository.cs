using netcoreAPI.Context;

namespace netcoreAPI.Repositories
{
    public class BaseRepository
    {
        protected readonly AppDbContext DbContext;

        public BaseRepository(AppDbContext dbContext)
        {
            DbContext = dbContext;
            DbContext.Database.EnsureCreated();
        }


    }
}
