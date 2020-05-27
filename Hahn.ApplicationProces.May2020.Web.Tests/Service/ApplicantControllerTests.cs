using System;
using Hahn.ApplicatonProcess.May2020.Data;
using Hahn.ApplicatonProcess.May2020.Domain;
using Hahn.ApplicatonProcess.May2020.Web.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Hahn.ApplicationProces.May2020.Web.Tests
{
    public class ApplicantServiceTests
    {
        private readonly DbContext context;
        public ApplicantServiceTests()
        {
            context = new ApplicantContext(options());
        }

        [Fact]
        public void Test_Get_Returns_Valid_Applicant_For_Given_Id()
        {

        }

        [Fact]
        public void Test_Name_Is_Atleast_Five_Characters_Validation_Runs()
        {

        }

        [Fact]
        public void Test_FamilyName_Is_Atleast_Five_Characters_Validation_Runs()
        {

        }

        [Fact]
        public void Test_Address_Atleast_Ten_Characters_Validation_Runs()
        {

        }

        [Fact]
        public void Test_Country_Name_Is_Valid_Validation_Runs()
        {

        }

        [Fact]
        public void Test_Email_Address_Validation_Runs()
        {

        }

        [Fact]
        public void Test_Age_Is_Between_20_And_60_Validation_Runs()
        {

        }

        [Fact]
        public void Test_Hired_Is_Not_Null_When_Provided_Validation_Runs()
        {

        }

        [Fact]
        public void Test_Get_Throws_Exception_When_Applicant_Not_Found()
        {

        }

        [Fact]
        public void Test_Create_Creates_Applicant_Without_Issues()
        {

        }

        [Fact]
        public void Test_Update_Returns_False_When_Applicant_Not_Present()
        {

        }

        [Fact]
        public void Test_Update_Updates_Applicant_Without_Issues()
        {

        }

        [Fact]
        public void Test_Delete_Deletes_Applicant_Without_Issues_When_Present()
        {

        }

        [Fact]
        public void Test_Delete_Returns_FalseWhen_Applicant_Not_Present()
        {

        }


        private Applicant Applicant()
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

        private IApplicantService Service()
            => new ApplicantService();
        private DbContextOptions<ApplicantContext> options()
        {
            return new DbContextOptionsBuilder<ApplicantContext>().
                UseInMemoryDatabase("Applicant").
                Options;
        }
    }
}
