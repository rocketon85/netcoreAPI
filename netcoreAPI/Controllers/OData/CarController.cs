using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.SignalR;
using netcoreAPI.Domains;
using netcoreAPI.Hubs;
using netcoreAPI.Repositories;
using netcoreAPI.Services;

namespace netcoreAPI.Controllers.OData
{
    [ApiVersion(3.0)]
    [ControllerName("car")]
    [Route("api/odata/v{version:apiVersion}/car")]
    public class CarController : ODataController
    {
        private readonly ILogger<CarController> _logger;
        private readonly CarRepository _carRepository;
        private readonly ICarService _carService;
        private readonly IHubContext<SignalRHub> _hubContext;
        private readonly IMapper _mapper;

        public CarController(ILogger<CarController> logger, CarRepository carRepository, ICarService carService, IHubContext<SignalRHub> hubContext, IMapper mapper)
        {
            _logger = logger;
            _carRepository = carRepository;
            _carService = carService;
            _hubContext = hubContext;
            _mapper = mapper;
        }

        [EnableQuery]
        [HttpGet("cars")]
        [ProducesResponseType(typeof(IQueryable<CarDomain>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public ActionResult<IQueryable<CarDomain>> Get(ODataQueryOptions<CarDomain> options, ApiVersion version)
        {
            var cars = _carRepository.GetAllQueryable();
            return cars != null ? Ok(cars) : NotFound(1);
        }
    }
}