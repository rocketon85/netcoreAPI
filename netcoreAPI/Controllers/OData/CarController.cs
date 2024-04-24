using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.SignalR;
using netcoreAPI.Domains;
using netcoreAPI.Hubs;
using netcoreAPI.Repositories;

namespace netcoreAPI.Controllers.OData
{
    [ApiVersion(3.0)]
    [ControllerName("car")]
    [Route("api/odata/v{version:apiVersion}/car")]
    public class CarController : ODataController
    {
        private readonly ILogger<CarController> logger;
        private readonly IRepositoryWrapper repository;
        private readonly IHubContext<SignalRHub> hubContext;
        private readonly IMapper mapper;

        public CarController(ILogger<CarController> logger, IRepositoryWrapper repository, IHubContext<SignalRHub> hubContext, IMapper mapper)
        {
            this.logger = logger;
            this.repository = repository;
            this.hubContext = hubContext;
            this.mapper = mapper;
        }

        [EnableQuery]
        [HttpGet("cars")]
        [ProducesResponseType(typeof(IQueryable<CarDomain>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public ActionResult<IQueryable<CarDomain>> Get(ODataQueryOptions<CarDomain> options, ApiVersion version)
        {
            var cars = repository.Car.FindAll();
            return cars != null ? Ok(cars) : NotFound(1);
        }
    }
}