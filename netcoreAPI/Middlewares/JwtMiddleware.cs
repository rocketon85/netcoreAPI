using netcoreAPI.Services;
using netcoreAPI.Structures;

namespace netcoreAPI.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtService jwtService)
        {
            var token = context.Request.Headers[EnviromentSettings.SecuritySchemeName].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtService.ValidateJwtToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = userService.GetById(userId.Value);
            }

            await _next(context);
        }
    }
}
