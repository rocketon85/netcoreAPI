using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using netcoreAPI.Domains;
using netcoreAPI.Models.V1;
using netcoreAPI.Repositories;
using netcoreAPI.Services;

namespace netcoreAPI.Controllers.V1
{
    [ApiController]
    [Authorize]
    [ApiVersion(1.0, Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;
        private readonly CarRepository _carRepository;
        private readonly ICarService _carService;
        private readonly IMapper _mapper;

        public CarController(ILogger<CarController> logger, CarRepository carRepository, ICarService carService, IMapper mapper)
        {
            _logger = logger;
            _carRepository = carRepository;
            _carService = carService;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CarViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<CarViewModel>> Get(int id)
        {
            var car = await _carRepository.GetById(id);
            return car != null ? Ok(_mapper.Map<CarViewModel>(car)) : NotFound(id);
        }

        [HttpGet("cars")]
        [ProducesResponseType(typeof(IEnumerable<CarViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<CarViewModel>>> GetAll([FromServices] BrandRepository brandRepository)
        {
            var cars = await _carRepository.GetAll();
            var carView = _mapper.Map<List<CarViewModel>>(cars);
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
            var result = await _carService.CreateCar(_mapper.Map<CarDomain>(model));
            return result?.Id > 0 ? Ok(_mapper.Map<CarViewModel>(result)) : BadRequest(result);
        }
    }
}