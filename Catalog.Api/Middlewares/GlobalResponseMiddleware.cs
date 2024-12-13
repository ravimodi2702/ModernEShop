using Catalog.Api.DTO;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Threading.Tasks;

namespace Catalog.Api.Middlewares
{
    public class GlobalResponseMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
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
            var response = new ResponseWrapper(context.Response.StatusCode.ToString(), null, result);
            

            var resObject = JsonConvert.SerializeObject(response);

            // returing response to caller
            await context.Response.WriteAsync(resObject);
        }
    }
}
