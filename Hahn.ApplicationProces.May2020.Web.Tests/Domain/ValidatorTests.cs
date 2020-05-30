using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;

namespace Hahn.ApplicationProces.May2020.Tests.Domain
{
    public class ValidatorTests : IDisposable
    {
        private readonly TestObjectFactory test;
        public ValidatorTests()
        {
            test = new TestObjectFactory();
        }

        public void Dispose()
        {
            test.Dispose();
        }

        [Fact]
        public void Validation_Fails_For_Null_Input()
        {
            var v = test.validator();
            Assert.Throws<ArgumentNullException>(() =>
            {
                v.Validate(null, out _);
            });
        }

        [Theory]
        [InlineData("a")]
        [InlineData("")]
        public void Validation_Fails_For_Invalid_Name(string value)
        {
            var v = test.validator();
            var entity = test.applicant();
            entity.Name = value;
            var valid = v.Validate(entity, out IEnumerable<string> errors);
            Assert.False(valid);
            Assert.NotEmpty(errors);

        }

        [Theory]
        [InlineData("Soundararajan Dhakshinamoorthy")]
        public void Validation_Passes_For_Valid_Name(string value)
        {
            var v = test.validator();
            var entity = test.applicant();
            entity.Name = value;
            var valid = v.Validate(entity, out IEnumerable<string> errors);
            Assert.True(valid);
            Assert.Empty(errors);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("")]
        public void Validation_Fails_For_Invalid_FamilyName(string value)
        {
            var v = test.validator();
            var entity = test.applicant();
            entity.FamilyName = value;
            var valid = v.Validate(entity, out IEnumerable<string> errors);
            Assert.False(valid);
            Assert.NotEmpty(errors);

        }

        [Theory]
        [InlineData("Soundararajan Dhakshinamoorthy")]
        public void Validation_Passes_For_Valid_FamilyName(string value)
        {
            var v = test.validator();
            var entity = test.applicant();
            entity.FamilyName = value;
            var valid = v.Validate(entity, out IEnumerable<string> errors);
            Assert.True(valid);
            Assert.Empty(errors);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("")]
        public void Validation_Fails_For_Invalid_Address(string value)
        {
            var v = test.validator();
            var entity = test.applicant();
            entity.Address = value;
            var valid = v.Validate(entity, out IEnumerable<string> errors);
            Assert.False(valid);
            Assert.NotEmpty(errors);
        }

        [Theory]
        [InlineData("Dusselorf, Germany, Europe")]
        public void Validation_Passes_For_Valid_Address(string value)
        {
            var v = test.validator();
            var entity = test.applicant();
            entity.Address = value;
            var valid = v.Validate(entity, out IEnumerable<string> errors);
            Assert.True(valid);
            Assert.Empty(errors);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(-10)]
        public void Validation_Fails_For_Invalid_Age(int value)
        {
            var v = test.validator();
            var entity = test.applicant();
            entity.Age = value;
            var valid = v.Validate(entity, out IEnumerable<string> errors);
            Assert.False(valid);
            Assert.NotEmpty(errors);
        }

        [Theory]
        [InlineData(20)]
        [InlineData(60)]
        public void Validation_Passes_For_Valid_Age(int value)
        {
            var v = test.validator();
            var entity = test.applicant();
            entity.Age = value;
            var valid = v.Validate(entity, out IEnumerable<string> errors);
            Assert.True(valid);
            Assert.Empty(errors);
        }

        [Theory]
        [InlineData("soundararajanoutlook.com")]
        [InlineData("@soundararrajan.outlook.com")]
        [InlineData("soundararrajan.outlook.com@")]
        public void Validation_Fails_For_Invalid_Email(string value)
        {
            var v = test.validator();
            var entity = test.applicant();
            entity.EMailAdress = value;
            var valid = v.Validate(entity, out IEnumerable<string> errors);
            Assert.False(valid);
            Assert.NotEmpty(errors);
        }

        [Theory]
        [InlineData("soundararajan@outlook.com")]
        [InlineData("er.soundararajan@hotmail.com")]
        public void Validation_Passes_For_Valid_Email(string value)
        {
            var v = test.validator();
            var entity = test.applicant();
            entity.EMailAdress = value;
            var valid = v.Validate(entity, out IEnumerable<string> errors);
            Assert.True(valid);
            Assert.Empty(errors);
        }
    }
}
