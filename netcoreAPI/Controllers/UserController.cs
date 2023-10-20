using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using netcoreAPI.Models;
using netcoreAPI.Services;

namespace netcoreAPI.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion(2.0)]
    [ApiVersion(1.0, Deprecated = true)]
    //if we want to add support localization in route
    //[Route("api/v{version:apiVersion}/{culture:culture}/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController: ControllerBase
    {
        private readonly IStringLocalizer<UserController> localizer;

        private readonly ILogger<UserController> logger;
        private readonly IUserService userService;

        public UserController(ILogger<UserController> logger, IStringLocalizer<UserController> localizer, IUserService userService)
        {
            this.localizer = localizer;
            this.logger = logger;
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(AuthRespModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<AuthRespModel>> Authenticate(AuthRequest model)
        {
            var response = await this.userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = this.localizer["wrongUserPassword"].Value});

            return Ok(response);
        }

    }
}
