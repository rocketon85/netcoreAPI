//using netcoreAPI.Dal;
//using netcoreAPI.Repository;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace netcoreAPI.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public abstract class ControllerBaseCustom: ControllerBase
//    {

//        protected readonly ILogger<ControllerBaseCustom> logger;
//        protected readonly BaseRepository< repository;

//        public ControllerBaseCustom(ILogger<ControllerBaseCustom> logger, IRepository repository)
//        {
//            this.logger = logger;
//            this.repository = repository;
//        }

//        public abstract Task<IActionResult> GetAll();
//    }
//}