using Data;
using Domain;
using System;
using System.Text;
using Api.Controllers;
using System.Net.Http;
using System.Text.Json;
using Data.Validations;
using Domain.Validations;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Tests
{
    internal class TestObjectFactory : IDisposable
    {
        /// <summary>
        /// ran for each test
        /// </summary>
        internal TestObjectFactory()
        {
            //Though we create a new ApplicationContext every time
            //it doesn't get created every time. It is initialized 
            //once for an AppDomain. But we manually Ensure
            //Creation in coustructor, because we manually dispose 
            //it for every test run.
            var context = new ApplicantContext();
            context.Database.EnsureCreated();
        }

        /// <summary>
        /// Ran after each test. 
        /// </summary>
        public void Dispose()
        {
            //the database need to be cleaned up post every run
            //to cleanup the data setup for the previous test run
            var context = new ApplicantContext();
            context.Database.EnsureDeleted();
        }

        internal ILogger<ApplicantController> logger()
        {
            return new NullLogger<ApplicantController>();
        }

        internal IApplicantService service()
        {
            return new ApplicantService(validator(), db());
        }

        internal IApplicantRepository db()
        {
            return new ApplicantRepository();
        }

        internal ApplicantController controller()
        {
            var c = new ApplicantController(logger(), service());
            return c;
        }

        internal IApplicantValidator validator()
        {
            return new ApplicantValidator();
        }

        internal ByteArrayContent request(Applicant a)
        {
            var bytes = Encoding.UTF8.GetBytes(applicantJson(a));
            var content = new ByteArrayContent(bytes);
            content.Headers.ContentType =
                new MediaTypeHeaderValue("application/json");
            return content;
        }

        internal string applicantJson(Applicant a)
        {
            return JsonSerializer.Serialize(a, typeof(Applicant));
        }

        internal string applicantJson()
        {
            return JsonSerializer.Serialize(applicant(), typeof(Applicant));
        }

        internal Applicant invalidApplicant()
        {
            return new Applicant()
            {
                ID = 100,
                Name = "a",
                FamilyName = "b",
                Address = "find",
                EMailAdress = "duck",
                Age = 1,
                CountryOfOrigin = "India",
                Hired = false
            };
        }


        internal Applicant applicant()
        {
            return new Applicant()
            {
                ID = 100,
                Name = "Soundararajan",
                FamilyName = "Dhakshinamoorthy",
                Address = "47, Jones Cassia, Karanai Main Road, Ottiaymbakkam",
                Age = 35,
                CountryOfOrigin = "India",
                EMailAdress = "soundararajan@outlook.com",
                Hired = default(bool)
            };
        }

        internal Applicant updated()
        {
            var a = applicant();
            a.Address = $"updated_address";
            a.Name = $"updated_name";
            return a;
        }

        internal Applicant updatedInvalid()
        {
            var a = applicant();
            a.Address = string.Empty;
            return a;
        }

        internal Applicant applicant(string json)
        {
            //The deserialization of the json from HttpResponse fails
            //if the naming policy is not set explicitly.
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var obj = JsonSerializer.Deserialize<Applicant>(json, options);
            return obj;
        }
    }
}

