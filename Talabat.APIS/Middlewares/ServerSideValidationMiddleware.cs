using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Text.Json;
using Talabat.APIS.Errors;

namespace Talabat.APIS.Middlewares
{
    public class ServerSideValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ServerSideValidationMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ServerSideValidationMiddleware(RequestDelegate next, ILogger<ServerSideValidationMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Call the next middleware in the pipeline
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An unhandled exception has occurred.");

                // Set the response content type
                context.Response.ContentType = "application/json";

                // Create a response object with error details
                var response = new ExceptionServerHandling()
                {
                    Details = _env.IsDevelopment() ? ex.StackTrace : null // Include stack trace only in development
                };

                // Write the response
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
