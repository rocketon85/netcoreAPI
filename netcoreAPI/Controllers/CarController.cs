using netcoreAPI.Dal;
using netcoreAPI.Domain;
using netcoreAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace netcoreAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class CarController: ControllerBase 
    {
        private readonly ILogger<CarController> logger;
        private readonly CarRepository repository;

        public CarController(ILogger<CarController> logger, CarRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }


        [HttpGet("car")]
        public async Task<IActionResult> Get()
        {
            var a = this.repository.GetAll();
            return Ok(a);
        }

        [HttpGet("cars")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAll([FromServices] BrandRepository brandRepository)
        {
            var a = this.repository.GetAll();
            return Ok(a);
        }
    }
}