using System;
using System.Collections.Generic;
using System.Linq;
using Api.Documentation;
using Xunit;

namespace Tests.Example
{
    public class ExampleTests
    {
        TestObjectFactory test;
        public ExampleTests()
        {
            test = new TestObjectFactory();
        }

        [Fact]
        public void ApplicantExample_Is_Not_Null()
        {
            ApplicantExample example = new ApplicantExample();
            Assert.NotNull(example);
        }

        [Fact]
        public void ApplicantExample_Passes_Validation()
        {
            ApplicantExample ex = new ApplicantExample();

            var validator = test.validator();
            var success = validator.Validate(ex.GetExamples(), out _);
            Assert.True(success);
        }
    }
}
