using AutoMapper;
using netcoreAPI.Context;
using netcoreAPI.Extensions;
using netcoreAPI.Repositories;

namespace netcoreAPI.Tests.Controllers
{
    [Collection("Enviroment collection")]
    public class BaseController
    {
        protected IRepositoryWrapper Repository;
        protected readonly IMapper Mapper;

        public BaseController()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            Mapper = config.CreateMapper();
        }
    }
}
