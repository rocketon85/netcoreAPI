using netcoreAPI.Models;
using System.Net;
using System.Web.Http.ExceptionHandling;


namespace netcoreAPI.Middlewares
{

    class ExceptionHandlerMiddleware : IExceptionHandler
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task InvokeAsync(HttpContext httpContext, ILogger<IExceptionHandler> logger)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception occured: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsJsonAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error."
            });
        }
    }
}
