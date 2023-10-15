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
using AutoMapper;

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
        private readonly IMapper mapper;

        public CarController(ILogger<CarController> logger, CarRepository carRepository, ICarService carService, IHubContext<SignalRHub> hubContext, IMapper mapper)
        {
            this.logger = logger;
            this.carRepository = carRepository;
            this.carService = carService;
            this.hubContext = hubContext;
            this.mapper = mapper;
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
            var carView = this.mapper.Map<List<CarViewModel>>(cars);
            return carView.Any() ? Ok(carView) : NotFound();
        }

        [HttpPost("add")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<CarViewModel>> CreateCar([FromBody] CarCreate model)
        {
            var result = await this.carService.CreateCar(this.mapper.Map<Car>(model));

            this.hubContext?.Clients.All.SendCoreAsync("newCar", new[] { "auto nuevo" });
            return result?.Id > 0 ? Ok(this.mapper.Map<CarViewModel>(result)) : BadRequest(result);
        }
    }
}  