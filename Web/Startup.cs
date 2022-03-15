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
using AspNetCore.Authentication.ApiKey;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api
{
    public class Startup
    {
        public static readonly string version = "v1";
        private static readonly string title = "ASP .NET Core 360";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var user = GetUserMetadata();
            services.AddControllers();

            services.AddDbContext<ApplicantContext>();
            services.AddScoped<ApplicantContext>();
            services.AddScoped<IApplicantValidator, ApplicantValidator>();
            services.AddScoped<IApplicantRepository, ApplicantRepository>();
            services.AddScoped<IApplicantService, ApplicantService>();

            //services.AddAuthentication(ApiKeyDefaults.AuthenticationScheme)
            //.AddApiKeyInHeaderOrQueryParams(options =>
            //{
            //    options.Realm = Configuration["NetCore360:ApiKey"];
            //    options.KeyName = "X-API-KEY";
            //});

            //services.AddAuthorization(options =>
            //{
            //    options.FallbackPolicy = new AuthorizationPolicyBuilder()
            //        .RequireAuthenticatedUser()
            //        .Build();
            //});
            services.AddSwaggerGen(sw =>
            {
                ConfigureSwaggerGen(sw);
            });
            services.AddSwaggerExamplesFromAssemblyOf<ApplicantExample>();
        }

        private class UserMetadata
        {
            public string Owner { get; set; }
            public string ApiKey { get; set; }
            public string Issuer { get; set; }
        }

        private UserMetadata GetUserMetadata()
        {
            return new UserMetadata()
            {
                Owner = Configuration["NetCore360:ApiKeyDefaultOwner"],
                ApiKey = Configuration["NetCore360:ApiKey"],
                Issuer = "Localhost-AspNetCore360"
            };
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
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
