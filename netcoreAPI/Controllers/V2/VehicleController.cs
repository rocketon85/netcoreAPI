using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using netcoreAPI.Domains;
using netcoreAPI.Hubs;
using netcoreAPI.Repositories;
using netcoreAPI.Structures;
using netcoreAPI.Contracts.Models.Requests.V2;
using netcoreAPI.Contracts.Models.Responses.V2;

namespace netcoreAPI.Controllers.V2
{
    [ApiController]
    [Authorize]
    [ApiVersion(2.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly ILogger<VehicleController> logger;
        private readonly IRepositoryWrapper repository;
        private readonly IHubContext<SignalRHub> hubContext;
        private readonly IMapper mapper;

        public VehicleController(ILogger<VehicleController> logger, IRepositoryWrapper repository, IHubContext<SignalRHub> hubContext, IMapper mapper)
        {
            this.logger = logger;
            this.repository = repository;
            this.hubContext = hubContext;
            this.mapper = mapper;
        }

        //make endpoints for calculate km/l
        /* agregar endpoint para calcular km/l, recive el id del vehiculo, vel avg, presion neumatico
         * obtiene el 
         * vehiculo, auto, moto, obtiene el consumo basico del vehiculo, segun tipo, cant de ruedas, peso del vehiculo, segun la velocidad promedio
         * aplica un consumo segun el combustible
        */

        /* Vehicle
         *  -> terrestre (cant ruedas, combustible nafta, diesel, electric)
         *          -> auto (add behivor motor)
         *          -> moto (add behivor motor)
         *          -> triciclo (add behivor motor)
         *          ->bicicleta
         *  -> maritimo
         *      -> lancha (add behivor motor)
         *      -> buque (add behivor motor)
         *      -> velero (add or notbehivor motor)
         *      -> bote
         */


        [HttpGet("calculate_distance")]
        [ProducesResponseType(typeof(IEnumerable<CarViewResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<CarViewResponse>>> GetAll([FromServices] BrandRepository brandRepository)
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
            //var result = await repository.Car.Create(mapper.Map<CarDomain>(model));
            var domain = mapper.Map<CarDomain>(model);
            await repository.Car.CreateAsync(domain);
            hubContext?.Clients.All.SendCoreAsync(HubMessages.NewCar, new[] { model });
            return domain.Id > 0 ? Ok(mapper.Map<CarViewResponse>(domain)) : BadRequest(domain);
        }
    }
}