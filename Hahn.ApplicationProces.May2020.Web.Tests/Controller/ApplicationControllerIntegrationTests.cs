using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Hahn.ApplicationProces.May2020.Tests;
using Hahn.ApplicatonProcess.May2020.Data;
using Hahn.ApplicatonProcess.May2020.Domain;
using Hahn.ApplicatonProcess.May2020.Web;
using Hahn.ApplicatonProcess.May2020.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Hahn.ApplicationProces.May2020.Web.Tests
{
    public class ApplicationControllerIntegrationTests :
        IClassFixture<WebApplicationFactory<Startup>>, IDisposable
    {
        private readonly TestObjectFactory test;
        private readonly WebApplicationFactory<Startup> factory;

        public ApplicationControllerIntegrationTests
            (WebApplicationFactory<Startup> factory)
        {
            test = new TestObjectFactory();
            this.factory = factory;
        }

        public void Dispose()
        {
            test.Dispose();
            //Don't dispose WebApplicationFactory it get's injected only once 
            //per Test class.
        }


        [Theory]
        [InlineData("/api/v1/applicant")]
        public async Task Post_Returns_201_And_URL_On_Succes(string url)
        {
            var expected = test.applicant();

            var content = test.request(expected);
            var client = factory.CreateClient();

            var response = await client.PostAsync(url, content);

            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == HttpStatusCode.Created);
        }


        [Theory]
        [InlineData("/api/v1/applicant")]
        public async Task Post_Returns_400_On_Validation_Failure(string url)
        {
            var content = test.request(test.invalidApplicant());
            var client = factory.CreateClient();
            var response = await client.PostAsync(url, content);

            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        }


        [Theory]
        [InlineData("/api/v1/applicant")]
        public async Task Post_Returns_409_When_Obj_Exists(string url)
        {
            var content = test.request(test.applicant());
            var client = factory.CreateClient();
            var task = await client.PostAsync(url, content)
                .ContinueWith(x => client.PostAsync(url, content));
            Assert.True(task.Result.StatusCode == HttpStatusCode.Conflict);
        }

        [Theory]
        [InlineData("/api/v1/applicant")]
        public async Task Post_Returns_400_When_Input_Is_Null(string url)
        {
            var applicant = test.request(null);
            var client = factory.CreateClient();
            var response = await client.PostAsync(url, applicant);

            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("/api/v1/applicant")]
        public async Task Get_Returns_200_On_Success(string url)
        {
            var expected = test.applicant();
            test.db().Add(expected);

            var client = factory.CreateClient();
            url = string.Join("/", url, expected.ID);
            var response = await client.GetAsync(url);

            Assert.True(response.StatusCode == HttpStatusCode.OK);
            var json = await response.Content.ReadAsStringAsync();
            var actual = test.applicant(json);
            Assert.True(expected.Equals(actual));
        }

        [Theory]
        [InlineData("/api/v1/applicant")]
        public async Task Get_Returns_404_When_Obj_Doesnt_Exist(string url)
        {
            var expected = test.applicant();

            var client = factory.CreateClient();
            url = string.Join("/", url, expected.ID);
            var response = await client.GetAsync(url);

            Assert.True(response.StatusCode == HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData("/api/v1/applicant")]
        public async Task Put_Returns_400_When_Input_Is_Null(string url)
        {
            var content = test.request(null);
            var client = factory.CreateClient();
            var response = await client.PutAsync(url, content);

            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("/api/v1/applicant")]
        public async Task Put_Returns_404_When_Obj_Doesnt_Exist(string url)
        {
            var expected = test.applicant();

            var client = factory.CreateClient();
            var request = test.request(test.updated());
            var response = await client.PutAsync(url, request);

            Assert.True(response.StatusCode == HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData("/api/v1/applicant")]
        public async Task Put_Returns_400_When_Obj_Is_Invalid(string url)
        {
            var expected = test.applicant();
            test.db().Add(expected);

            var client = factory.CreateClient();
            var request = test.request(test.updatedInvalid());
            var response = await client.PutAsync(url, request);

            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("/api/v1/applicant")]
        public async Task Delete_Returns_409_When_Obj_Doesnt_Exist(string url)
        {
            var client = factory.CreateClient();
            url = string.Join("/", url, test.applicant().ID);
            var response = await client.DeleteAsync(url);

            Assert.True(response.StatusCode == HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData("/api/v1/applicant")]
        public async Task Delete_Returns_200_On_Successful_Deletion(string url)
        {
            var expected = test.applicant();
            test.db().Add(expected);

            var client = factory.CreateClient();
            url = string.Join("/", url, expected.ID);
            var response = await client.DeleteAsync(url);

            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }
    }
}