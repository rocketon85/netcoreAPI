using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using netcoreAPI.Domains;
using netcoreAPI.Models.V1;
using netcoreAPI.Repositories;

namespace netcoreAPI.Controllers.V1
{
    [ApiController]
    [Authorize]
    [ApiVersion(1.0, Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> logger;
        private readonly IRepositoryWrapper repository;
        private readonly IMapper mapper;

        public CarController(ILogger<CarController> logger, IRepositoryWrapper repository, IMapper mapper)
        {
            this.logger = logger;
            this.repository = repository;
            this.mapper = mapper;
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CarViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<CarViewModel>> Get(int id)
        {
            var car = await repository.Car.FindByCondition(p=> p.Id == id).FirstOrDefaultAsync();
            return car != null ? Ok(mapper.Map<CarViewModel>(car)) : NotFound(id);
        }

        [HttpGet("cars")]
        [ProducesResponseType(typeof(IEnumerable<CarViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<CarViewModel>>> GetAll()
        {
            var cars = await repository.Car.FindAll().ToListAsync();
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
            //var result = await repository.Car.CreateAsync(mapper.Map<CarDomain>(model));
            var domain = mapper.Map<CarDomain>(model);
            await repository.Car.CreateAsync(domain);
            return domain.Id > 0 ? Ok(mapper.Map<CarViewModel>(domain)) : BadRequest(domain);
        }
    }
}