using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using netcoreAPI.Domain;
using netcoreAPI.Models.V1;
using netcoreAPI.Repository;
using netcoreAPI.Services;

namespace netcoreAPI.Controllers.V1
{
    [ApiController]
    [Authorize]
    [ApiVersion(1.0, Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> logger;
        private readonly CarRepository carRepository;
        private readonly ICarService carService;
        private readonly IMapper mapper;

        public CarController(ILogger<CarController> logger, CarRepository carRepository, ICarService carService, IMapper mapper)
        {
            this.logger = logger;
            this.carRepository = carRepository;
            this.carService = carService;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CarViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<CarViewModel>> Get(int id)
        {
            var car = await carRepository.GetById(id);
            return car != null ? Ok(mapper.Map<CarViewModel>(car)) : NotFound(id);
        }

        [HttpGet("cars")]
        [ProducesResponseType(typeof(IEnumerable<CarViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<CarViewModel>>> GetAll([FromServices] BrandRepository brandRepository)
        {
            var cars = await carRepository.GetAll();
            var carView = mapper.Map<List<CarViewModel>>(cars);
            return carView.Any() ? Ok(carView) : NotFound();
        }

        [HttpPost("add")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(CarViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<CarViewModel>> CreateCar([FromBody] CarCreateModel model)
        {
            var result = await carService.CreateCar(mapper.Map<Car>(model));

            return result?.Id > 0 ? Ok(mapper.Map<CarViewModel>(result)) : BadRequest(result);
        }
    }
}