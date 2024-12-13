using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Application.HttpClient;
using Test.Application.Repositories;
using Test.Application.UseCases.GetData;
using Test.Infrastructure.HttpClient;
using Test.Infrastructure.Repositories;

namespace Test.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(typeof(GetAllDataHandler)));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddValidatorsFromAssembly(typeof(GetAllDataValidator).Assembly);
            services.AddHttpClient<ITestHttpClient>("httpclient", context =>
            {
                context.BaseAddress = new Uri("https://reqres.in/api/");
            }).AddHttpMessageHandler<TestHttpClientHandler>();

            services.AddScoped<TestHttpClientHandler>();
            services.AddScoped<ITestHttpClient, TestHttpClient>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseMiddleware<Test.Api.Middlewares.ExceptionHandlerMiddleware>();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
