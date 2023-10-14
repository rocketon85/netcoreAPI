using netcoreAPI.Dal;
using netcoreAPI.Domain;
using netcoreAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using netcoreAPI.Models;
using System.Runtime.CompilerServices;
using netcoreAPI.Services;
using Microsoft.AspNetCore.SignalR;
using netcoreAPI.Hubs;

namespace netcoreAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class CarController: ControllerBase 
    {
        private readonly ILogger<CarController> logger;
        private readonly CarRepository carRepository;
        private readonly ICarService carService;
        private readonly IHubContext<SignalRHub> hubContext;

        public CarController(ILogger<CarController> logger, CarRepository carRepository, ICarService carService, IHubContext<SignalRHub> hubContext)
        {
            this.logger = logger;
            this.carRepository = carRepository;
            this.carService = carService;
            this.hubContext = hubContext;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> Get(int id)
        {
            var car = await this.carRepository.GetById(id);
            return car != null ? Ok(car) : NotFound(id);
        }

        [HttpGet("cars")]
        public async Task<ActionResult<IEnumerable<Car>>> GetAll([FromServices] BrandRepository brandRepository)
        {
            var cars = await this.carRepository.GetAll();
            var carView = cars.Select(p => new CarViewModel ( p.Id, p.Name, p.Fuel.Name, p.Brand.Name, p.Model.Name )).ToList();
            return carView.Any() ? Ok(carView) : NotFound();
        }

        [HttpPost("add")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<CarViewModel>> CreateCar([FromBody] CarCreate model)
        { 
            var result = await this.carService.CreateCar(
                new Car
                {
                    Name = model.Name ?? "",
                    BrandId = model.BrandId.GetValueOrDefault(),
                    ModelId = model.ModelId.GetValueOrDefault(),
                    FuelId = model.FuelId.GetValueOrDefault()
                }
                );

            this.hubContext?.Clients.All.SendCoreAsync("newCar", new[] { "auto nuevo" });
            return result == 1 ? Ok(model) : BadRequest(result);
        }
    }
}  