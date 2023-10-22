using Microsoft.Extensions.Options;
using netcoreAPI.Dal;
using netcoreAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcoreAPI.Tests.Services
{
    [Collection("Database collection")]
    public class BaseService
    {
        protected readonly AppDbContext dbContext;
        public BaseService(TestDbContext dbContext)
        {
            this.dbContext = dbContext;
            //var startUp = new StartUp();
        }
    }
}
