﻿using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using netcoreAPI.Contracts.Models.Requests;
using netcoreAPI.Contracts.Models.Responses;
using netcoreAPI.Extensions;
using netcoreAPI.Options;
using netcoreAPI.Repositories;
using netcoreAPI.Structures;

namespace netcoreAPI.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion(2.0)]
    [ApiVersion(1.0, Deprecated = true)]
    //add support localization in route
    //[Route("api/v{version:apiVersion}/{culture:culture}/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IStringLocalizer<UserController> localizer;
        private readonly ILogger<UserController> logger;
        private readonly IRepositoryWrapper repository;
         
        public UserController(ILogger<UserController> logger, IStringLocalizer<UserController> localizer, IRepositoryWrapper repository, IOptions<AzureOption> azureOption)
        {
            this.localizer = localizer;
            this.logger = logger;
            this.repository = repository;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(AuthorizationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<AuthorizationResponse>> Authenticate(AuthorizationRequest model)
        {
            var response = await repository.User.Authenticate(model);
            
            if (response == null)
                return BadRequest(new { message = localizer.GetValue<UserLanguage>(new UserLanguage(), UserLanguage.FieldWrongUserPassword) });
            return Ok(response);
        }
    }
}
