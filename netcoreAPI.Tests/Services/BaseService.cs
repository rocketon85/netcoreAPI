using netcoreAPI.Context;

namespace netcoreAPI.Tests.Services
{
    [Collection("Enviroment collection")]
    public class BaseService
    {
        protected readonly AppDbContext DbContext;
        public BaseService(TestDbContext dbContext)
        {
            DbContext = dbContext;
            //var startUp = new StartUp();
        }
    }
}
