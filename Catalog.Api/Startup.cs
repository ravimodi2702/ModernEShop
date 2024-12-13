using Catalog.Api.Middlewares;
using Catalog.Application.HttpClients;
using Catalog.Application.Repositories;
using Catalog.Application.UseCases.AddCategory;
using Catalog.Application.UseCases.GetAllCategories;
using Catalog.Infrastructure.HttpClients;
using Catalog.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
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

namespace Catalog.Api
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
            var mockApiSettings = Configuration.GetSection("MockApiSettings").Get<MockApiSettings>();
            services.Configure<MockApiSettings>(Configuration.GetSection("MockApiSettings"));
            services.AddControllers();
            services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(typeof(GetAllCategoriesQuery)));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());    
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddHttpClient("mockapi", httpclient =>
            {
                httpclient.BaseAddress = new Uri(mockApiSettings.BaseUrl);
            }).AddHttpMessageHandler<MockAPIHttpHandler>();

            services.AddScoped<MockAPIHttpHandler>();
            services.AddScoped<IMockApiClient, MockApiClient>();
            services.AddValidatorsFromAssemblyContaining(typeof(AddCategoryValidator));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
               // app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<GlobalResponseMiddleware>();
            app.UseMiddleware<ExcecptionHandlerMiddleware>();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
