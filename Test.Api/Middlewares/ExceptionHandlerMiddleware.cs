using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Test.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(ex));
            }

        }
    }
}
