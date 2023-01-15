using EShopAPI.Contracts;
using EShopAPI.Models;
using EShopAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;

namespace EShopAPI
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo { Title = "My First Swagger", Version = "V1" });
            });
            //services.AddSwaggerGen(swagger =>
            //{
            //    services.AddSwaggerGen(c =>
            //    {
            //        c.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo
            //        {
            //            Title = "My First Swagger",
            //            Version = "V1"
            //        });
            //    });

            //    swagger.IncludeXmlComments(Path.Combine(Directory.GetCurrentDirectory(), @"bin\Debug\netcoreapp3.1", "EshopApi.xml"));
            //});
            services.AddControllers();
            services.AddDbContext<EShopAPI_DBContext>(options =>
            {
                options.UseSqlServer("Data Source=.;Initial Catalog=EShopAPI_DB;Integrated Security=True;");
            });
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IProductsRepository, ProductsRepository>();
            services.AddTransient<ISalesPersonsRepository, SalesPersonsRepository>();
            services.AddResponseCaching();
            services.AddMemoryCache();
            //JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "http://localhost:40116",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("OurVerifyTopLearn"))

                    };
                }
                );
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCors", builder =>
                {
                    builder.SetIsOriginAllowed(origin => true)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .Build();
                });


            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseResponseCaching();
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/V1/swagger.json", "My First Swagger");
            });
            app.UseCors("EnableCors");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
