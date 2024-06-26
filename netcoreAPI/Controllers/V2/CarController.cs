using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using netcoreAPI.Domains;
using netcoreAPI.Hubs;
using netcoreAPI.Contracts.Models.Requests.V2;
using netcoreAPI.Contracts.Models.Responses.V2;
using netcoreAPI.Repositories;
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
        private readonly IRepositoryWrapper repository;
        //create ihubcontext for manage messages
        private readonly IHubContext<SignalRHub> hubContext;
        private readonly IMapper mapper;

        public CarController(ILogger<CarController> logger, IRepositoryWrapper repository, IHubContext<SignalRHub> hubContext, IMapper mapper)
        {
            this.logger = logger;
            this.repository = repository;
            this.hubContext = hubContext;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CarViewResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<CarViewResponse>> Get(int id)
        {
            var car = await repository.Car.FindByCondition(p=> p.Id == id).FirstOrDefaultAsync();
            return car != null ? Ok(mapper.Map<CarViewResponse>(car)) : NotFound(id);
        }

        [HttpGet("cars")]
        [ProducesResponseType(typeof(IEnumerable<CarViewResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<CarViewResponse>>> GetAll()
        {
            var cars = await repository.Car.FindAll().ToListAsync();
            var carView = mapper.Map<List<CarViewResponse>>(cars);
            return carView.Any() ? Ok(carView) : NotFound();
        }

        [HttpPost("add")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(CarViewResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<CarViewResponse>> CreateCar([FromBody] CarCreateRequest model)
        {
            var domain = mapper.Map<CarDomain>(model);
            await repository.Car.CreateAsync(domain);

            var mapped = mapper.Map<CarViewResponse>(domain);
            ((SignalRHub)hubContext).SendAllDataAsync(HubMessages.NewCar, new[] { mapped });
            return domain.Id > 0 ? Ok(mapped) : BadRequest(domain);
        }
    }
}