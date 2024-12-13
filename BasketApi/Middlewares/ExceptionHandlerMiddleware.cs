using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using System.Net;
using BasketApi.DTO;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using BasketApi.Controllers;
using Microsoft.VisualBasic;

namespace BasketApi.Middlewares
{

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, "something went wrong");
            context.Response.ContentType = "application/json";

            var response = new ResponseWrapper
            {
                StatusCode = context.Response.StatusCode,
                Error = "Generic error.",
                Data = ex,
                // Add other properties as needed
            };

            if (ex is ArgumentException)
            {
                response.StatusCode = 400;
                response.Error = "Invalid argument.";
            }
            else if (ex is DivideByZeroException)
            {
                response.StatusCode = 500;
                response.Error = "Divide By Zero.";
            }
            else if(ex is FluentValidation.ValidationException)
            {
                var exception = (FluentValidation.ValidationException)ex;
                response.StatusCode = 500;
                response.Error = JsonConvert.SerializeObject(exception.Errors);
            }

            context.Response.StatusCode = response.StatusCode;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
