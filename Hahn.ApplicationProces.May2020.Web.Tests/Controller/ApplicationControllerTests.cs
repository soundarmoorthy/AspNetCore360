using System;
using Hahn.ApplicatonProcess.May2020.Web.Controllers;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Hahn.ApplicationProces.May2020.Web.Tests
{
    public class ApplicationControllerTests
    {
        [Fact]
        public void Test_Controller_Returns_201_And_Object_URL_On_Succesful_Creation()
        {

        }

        [Fact]
        public void Test_Controller_Returns_400_On_Update_With_Invalid_Object_On_Post()
        {

        }

        [Fact]
        public void Test_Controller_Returns_400_On_Update_With_Invalid_Object_On_Put()
        {

        }

        [Fact]
        public void Test_Controller_Returns_500_On_Any_Generic_Error()
        {

        }

        [Fact]
        public void Test_Controller_Returns_Valid_API_Versions_When_Endpoint_Doesnt_Have_Version()
        {

        }


        private ApplicantController ApplicantController()
            => new ApplicantController(new NullLogger<ApplicantController>());
    }
}
