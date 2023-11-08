using Asp.Versioning;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using netcoreAPI.Extensions;
using netcoreAPI.Models;
using netcoreAPI.Options;
using netcoreAPI.Services;
using netcoreAPI.Structures;

namespace netcoreAPI.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion(2.0)]
    [ApiVersion(1.0, Deprecated = true)]
    //if we want to add support localization in route
    //[Route("api/v{version:apiVersion}/{culture:culture}/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IStringLocalizer<UserController> _localizer;
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IStringLocalizer<UserController> localizer, IUserService userService, IOptions<AzureOption> azureOption)
        {
            _localizer = localizer;
            _logger = logger;
            _userService = userService;

            
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(AuthRespModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<AuthRespModel>> Authenticate(AuthRequest model)
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = _localizer.GetValue<UserLanguage>(new UserLanguage(), UserLanguage.FieldWrongUserPassword) });
            return Ok(response);
        }
    }
}
