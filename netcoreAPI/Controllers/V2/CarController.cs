using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using netcoreAPI.Domains;
using netcoreAPI.Hubs;
using netcoreAPI.Models.V2;
using netcoreAPI.Repositories;
using netcoreAPI.Services;
using netcoreAPI.Structures;

namespace netcoreAPI.Controllers.V2
{
    [ApiController]
    [Authorize]
    [ApiVersion(2.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> logger;
        private readonly CarRepository carRepository;
        private readonly ICarService carService;
        private readonly IHubContext<SignalRHub> hubContext;
        private readonly IMapper mapper;

        public CarController(ILogger<CarController> logger, CarRepository carRepository, ICarService carService, IHubContext<SignalRHub> hubContext, IMapper mapper)
        {
            logger = logger;
            carRepository = carRepository;
            carService = carService;
            hubContext = hubContext;
            mapper = mapper;
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
            var result = await carService.CreateCar(mapper.Map<CarDomain>(model));
            hubContext?.Clients.All.SendCoreAsync(HubMessages.NewCar, new[] { model });
            return result?.Id > 0 ? Ok(mapper.Map<CarViewModel>(result)) : BadRequest(result);
        }
    }
}