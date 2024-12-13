using Basket.Application.IHttpHandlers;
using Basket.Application.Repositories;
using Basket.Application.UseCases.CreateShoppingCart;
using Basket.Infrastructure.HttpHandlers;
using Basket.Infrastructure.HttpHandlers.DiscountApiHandler;
using Basket.Infrastructure.Repositories;
using Basket.Infrastructure.Settings;
using BasketApi.Handlers.AuthorizationHandlers;
using BasketApi.Middlewares;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace BasketApi
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
            services.AddControllers();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Basket.Application.UseCases.CreateShoppingCart.CreateShoppingCartCommandHandler).Assembly));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IDiscountApiClient, DiscountApiClient>();
            services.AddValidatorsFromAssemblyContaining<CreateShoppingCartCommandValidator>();
            services.Configure<DiscountApiSettings>(this.Configuration.GetSection("DiscountApiSettings"));
            services.AddSwaggerGen();
            services.AddScoped<DiscountApiHttpHandler>();

            services.AddHttpClient("discountApiClient", (httpClient) =>
            {
                httpClient.BaseAddress = new Uri("https://localhost:7133");
            }).AddHttpMessageHandler<DiscountApiHttpHandler>();

            /*services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = Configuration["Auth0:Domain"];
                options.Audience = Configuration["Auth0:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = $"{Configuration["Auth0:Domain"]}",
                    ValidateAudience = true,
                    ValidAudience = Configuration["Auth0:Audience"],
                    ValidateLifetime = true,
                    NameClaimType = ClaimTypes.NameIdentifier, //generally it is named as claim "sub" but here we can give some other name as well,
                    RoleClaimType = ClaimTypes.Role
                };
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();

                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    }
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("canCreateBasket", policy => policy.Requirements.Add(new
                HasScopeRequirement("read:messages", Configuration["Auth0:Domain"])));
            });*/

            services.AddSingleton<IAuthorizationHandler, HasValidScopeHandler>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
            }



            app.UseHttpsRedirection();

            app.UseRouting();
            /*app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V2");
            });*/

            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V2");
            });
            app.UseMiddleware<ResponseWrapperMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();
          //  app.UseAuthentication();
           // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
