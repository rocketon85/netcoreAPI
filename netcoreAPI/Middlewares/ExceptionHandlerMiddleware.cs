using Microsoft.AspNetCore.Diagnostics;
using netcoreAPI.Contracts.Models.Responses;
using System.Net;


namespace netcoreAPI.Middlewares
{

    class ExceptionHandlerMiddleware : IExceptionHandler
    {
     
        public async ValueTask<bool> TryHandleAsync(HttpContext context,
                                                    Exception exception,
                                                    CancellationToken cancellation)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Your response object
            var error = new ErrorDetailsResponse()
            {
                StatusCode = context.Response.StatusCode,
                Message = @"Internal Server Error."
            };
            await context.Response.WriteAsJsonAsync(error, cancellation);
            return true;
        }

    }
}
