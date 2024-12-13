using BasketApi.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace BasketApi.Middlewares
{

    /// <summary>
    /// Response Wrapper Middleware to Request Delegate and handles Request/Response Customizations.
    /// </summary>
    public class ResponseWrapperMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<ResponseWrapperMiddleware> _logger;

        public ResponseWrapperMiddleware(RequestDelegate next, ILogger<ResponseWrapperMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        /// <summary>
        /// Invoke Method for the HttpContext
        /// </summary>
        /// <param name="context">The HTTP Context</param>
        /// <returns>Response</returns>
        public async Task Invoke(HttpContext context)
        {

            // Storing Context Body Response
            var currentBody = context.Response.Body;

            // Using MemoryStream to hold Controller Response
            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            // Passing call to Controller
            await _next(context);

            // Resetting Context Body Response
            context.Response.Body = currentBody;

            // Setting Memory Stream Position to Beginning
            memoryStream.Seek(0, SeekOrigin.Begin);

            // Read Memory Stream data to the end
            var readToEnd = new StreamReader(memoryStream).ReadToEnd();

            // Deserializing Controller Response to an object
            var result = JsonConvert.DeserializeObject(readToEnd);

            // Invoking Customizations Method to handle Custom Formatted Response
            var response = new ResponseWrapper
            {
                StatusCode = context.Response.StatusCode,
                Data = result,
                // Add other properties as needed
            };

            var resObject = JsonConvert.SerializeObject(response);

            _logger.LogInformation(resObject);
            // returing response to caller
            await context.Response.WriteAsync(resObject);

        }
    }
}
