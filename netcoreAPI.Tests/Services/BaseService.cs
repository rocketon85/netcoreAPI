using netcoreAPI.Context;

namespace netcoreAPI.Tests.Services
{
    [Collection("Database collection")]
    public class BaseService
    {
        protected readonly AppDbContext DbContext;
        public BaseService(TestDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
