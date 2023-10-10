using netcoreAPI.Dal;
using netcoreAPI.Domain;
using netcoreAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using netcoreAPI.Models;

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


        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> Get(int id)
        {
            var car = await this.repository.GetById(id);
            return car != null ? Ok(car) : NotFound(id);
        }

        [HttpGet("cars")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<Car>>> GetAll([FromServices] BrandRepository brandRepository)
        {
            var cars = await this.repository.GetAll();
            var carView = cars.Select(p => new CarViewModel ( p.Id, p.Fuel.Name, p.Brand.Name, p.Model.Name )).ToList();
            return carView.Any() ? Ok(carView) : NotFound();
        }
    }
}