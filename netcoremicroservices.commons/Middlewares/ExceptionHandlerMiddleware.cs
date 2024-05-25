using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using NetCoreMicroservices.Commons.Contracts.Responses;
using System.Net;

namespace NetCoreMicroservices.Commons.Middlewares
{
    public class ExceptionHandlerMiddleware : IExceptionHandler
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
                Message = @"Middleware Internal Server Error."
            };

            // Write log


            await context.Response.WriteAsJsonAsync(error, cancellation);
            return true;
        }

    }
}
