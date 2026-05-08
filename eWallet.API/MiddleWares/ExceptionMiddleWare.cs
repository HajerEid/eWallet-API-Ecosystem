using eWallet.API.Error;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace eWallet.API.MiddleWares
{
    public class ExceptionMiddleWare
    {
        public readonly RequestDelegate _next;
        public readonly ILogger<ExceptionMiddleWare> _logger;
        public readonly IHostEnvironment _environment;

        public ExceptionMiddleWare
            (RequestDelegate next,
            ILogger<ExceptionMiddleWare> logger,
            IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await _next(context);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
               
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _environment.IsDevelopment()?
                    new ApiException((int)HttpStatusCode.InternalServerError,
                                    ex.Message, ex.StackTrace)
                    : 
                    new ApiException((int)HttpStatusCode.InternalServerError);

                
                var options = new JsonSerializerOptions 
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        Converters = { new JsonStringEnumConverter() }
                };//obj>json
                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);


            }
        }



    }
}
