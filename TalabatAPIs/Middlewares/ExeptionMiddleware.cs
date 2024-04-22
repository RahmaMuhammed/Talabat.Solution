using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;
using System.Text.Json;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Middlewares
{
    public class ExeptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExeptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExeptionMiddleware(RequestDelegate next,ILogger<ExeptionMiddleware> logger,IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Take an action with the request

                await _next.Invoke(httpContext);

                // Take an action with the response
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message); // Development Env
                                             // log Exception in (Database | Files) // Production Env

                httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var response = _env.IsDevelopment() ?
                               new ApiExeptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                               : new ApiExeptionResponse((int)HttpStatusCode.InternalServerError);

                var json = JsonSerializer.Serialize(response);
                httpContext.Response.WriteAsync(json);

            }
        }
    }
}
