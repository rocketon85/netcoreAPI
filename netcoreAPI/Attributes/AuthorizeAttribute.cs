using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using netcoreAPI.Controllers;
using netcoreAPI.Extensions;
using netcoreAPI.Identity;
using netcoreAPI.Structures;

namespace netcoreAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IStringLocalizer<AuthorizeAttribute> _localizer;
        public AuthorizeAttribute(IStringLocalizer<AuthorizeAttribute> localizer)
        {
            _localizer = localizer;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            // authorization
            var user = (User?)context.HttpContext.Items[EnviromentVars.SessionUser];
            if (user == null)
            {
                // not logged in or role not authorized
                context.Result = new JsonResult(new { message = _localizer.GetValue<CommonLanguage>(new CommonLanguage(), CommonLanguage.FieldUnauthorized) }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
