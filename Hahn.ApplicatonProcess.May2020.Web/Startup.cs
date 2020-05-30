using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.May2020.Data;
using Hahn.ApplicatonProcess.May2020.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Hahn.ApplicatonProcess.May2020.Web
{
    public class Startup
    {
        public static readonly Version ApiVersion = new Version("1.0");
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

            services.AddApiVersioning(config =>
            {
                config.ReportApiVersions = true;
            });

            services.AddSwaggerGen(sw =>
            {
                ConfigureSwaggerGen(sw);
            });

        }

        private void ConfigureSwaggerGen(SwaggerGenOptions sw)
        {
            sw.SwaggerDoc(Startup.ApiVersion.ToString(), new OpenApiInfo
            {
                Title = "Hahn Applicant",
                Version = $"v{Startup.ApiVersion.Major}",
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
            var docXml = Path.Combine(System.AppContext.BaseDirectory,
                        "Hahn.ApplicatonProcess.May2020.Web.xml");
            sw.IncludeXmlComments(docXml);
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
                sw.SwaggerEndpoint("/swagger/v1/swagger.json", "Hahn Applicant");
                sw.RoutePrefix = string.Empty;
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
