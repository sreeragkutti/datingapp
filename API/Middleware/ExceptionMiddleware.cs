using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private RequestDelegate Next { get; }
        private ILogger<ExceptionMiddleware> Logger { get; }
        private IHostEnvironment Environment { get; }

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
                                   IHostEnvironment environment)
        {
            Next = next;
            Logger = logger;
            Environment = environment;
        }

       
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch(Exception excption)
            {
                Logger.LogError(excption, excption.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = Environment.IsDevelopment()
                                ? new ApiExceptions(context.Response.StatusCode, excption.Message, excption.StackTrace?.ToString())
                                : new ApiExceptions(context.Response.StatusCode, "Internal Server error") ;
                                
                var options = new JsonSerializerOptions();
                options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}