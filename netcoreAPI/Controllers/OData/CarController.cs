using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Query.Internal;
using netcoreAPI.Domain;
using netcoreAPI.Hubs;
using netcoreAPI.Models.V2;
using netcoreAPI.Repository;
using netcoreAPI.Services;
using System;

namespace netcoreAPI.Controllers.OData
{

    [ApiVersion(3.0)]
    //[ApiVersion(2.0)]
    [ControllerName("car")]
    [Route("api/odata/v{version:apiVersion}/car")]
    public class CarController : ODataController
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

        [EnableQuery]
        [HttpGet("cars")]
        [ProducesResponseType(typeof(IQueryable<Car>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public ActionResult<IQueryable<Car>> Get(ODataQueryOptions<Car> options, ApiVersion version)
        {
            var cars = carRepository.GetAllQueryable();
            return cars != null ? Ok(cars) : NotFound(1);
        }


    }
}