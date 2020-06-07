using Data;
using Domain;
using System;
using Data.Validations;
using Api.Documentation;
using Domain.Validations;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public class Startup
    {
        public static readonly string version = "v1";
        private static readonly string title = "ASP .NET Core 360 Application";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<ApplicantContext>();
            services.AddScoped<ApplicantContext>();
            services.AddScoped<IApplicantValidator, ApplicantValidator>();
            services.AddScoped<IApplicantRepository, ApplicantRepository>();
            services.AddScoped<IApplicantService, ApplicantService>();

            services.AddSwaggerGen(sw =>
            {
                ConfigureSwaggerGen(sw);
            });
            services.AddSwaggerExamplesFromAssemblyOf<ApplicantExample>();
            ;
        }

        private void ConfigureSwaggerGen(SwaggerGenOptions sw)
        {
            sw.ExampleFilters();
            sw.EnableAnnotations();
            sw.SwaggerDoc(version, new OpenApiInfo
            {
                Title = title,
                Version = version,
                Contact = new OpenApiContact()
                {
                    Name = "Soundar",
                    Email = "soundararajan@outlook.com",
                    Url = new Uri("https://soundararajan.in")
                },
                License = new OpenApiLicense
                {
                    Name = "Apache 2.0",
                    Url = new Uri("http://apache.org/licenses/LICENSE-2.0.html")
                }
            });
        }

        // This method gets called by the runtime. 
        //Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(sw =>
            {
                sw.SwaggerEndpoint
                ($"/swagger/{version}/swagger.json",
                title);
            });
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
