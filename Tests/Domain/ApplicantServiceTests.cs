using Xunit;
using Domain;
using System;
using Data.Validations;

namespace Tests.Domain
{
    public class ApplicantServiceTests : IDisposable
    {
        private readonly TestObjectFactory test;
        public ApplicantServiceTests()
        {
            test = new TestObjectFactory();
        }

        public void Dispose()
        {
            test.Dispose();
        }

        [Fact]
        public void Create_Throws_Validation_Failed_Exception_On_Null()
        {
            var service = test.service();
            Assert.Throws<ValidationFailedException>(() =>
            {
                service.Create(null);
            });

        }

        [Fact]
        public void Create_Throws_Validation_Failed_Exception_On_Invalid_Obj()
        {
            var service = test.service();
            Assert.Throws<ValidationFailedException>(() =>
            {
                service.Create(test.invalidApplicant());
            });
        }

        [Fact]
        public void
            Create_Throws_Applicant_Already_Exists_Exception_On_Duplicate()
        {
            test.db().Add(test.applicant());
            var service = test.service();
            Assert.Throws<ApplicantAlreadyExistsException>(() =>
            {
                service.Create(test.applicant());
            });
        }

        [Fact]
        public void Create_Suffesfully_Creates_Application()

        {
            var expected = test.applicant();
            var service = test.service();
            service.Create(expected);

            Assert.True(test.db().Get(expected.ID) != null);
        }

        [Fact]
        public void
            Delete_Throws_Applicant_Missing_Exception_When_Object_Not_Found()
        {
            var expected = test.applicant();
            test.db().Add(expected);
            Assert.Throws<ApplicantMissingExcpetion>(() =>
            {
                test.service().Delete(expected.ID + 1);
            });
        }

        [Fact]
        public void Delete_Deletes_The_Object_Succesfully_If_Exists()
        {
            var expected = test.applicant();
            test.db().Add(expected);
            test.service().Delete(expected.ID);

            Assert.True(test.db().Get(expected.ID) == null);
        }

        [Fact]
        public void Get_Throws_Applicant_Missing_Exception_When_Obj_Not_Found()
        {
            var expected = test.applicant();
            Assert.Throws<ApplicantMissingExcpetion>(() =>
            {
                test.service().Get(expected.ID);
            });
        }

        [Fact]
        public void Get_Returns_Value_succesfully_when_exists()
        {
            var expected = test.applicant();
            test.db().Add(expected);
            var actual = test.service().Get(expected.ID);
            Assert.True(expected.Equals(actual));
        }

        [Fact]
        public void Update_Throws_Argument_Null_Exception_On_Null_Input()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                test.service().Update(null);
            });
        }

        [Fact]
        public void Update_Throws_Applicant_Missing_Exception_When_Not_Found()
        {
            Assert.Throws<ApplicantMissingExcpetion>(() =>
            {
                test.service().Update(test.applicant());
            });
        }

        [Fact]
        public void Update_Throws_Validation_Failed_Exception_When_Invalid()
        {
            test.db().Add(test.applicant());
            Assert.Throws<ValidationFailedException>(() =>
            {
                test.service().Update(test.invalidApplicant());
            });
        }

        [Fact]
        public void Update_Updates_Succesfully_On_Valid_Input()
        {
            var expected = test.applicant();
            test.db().Add(expected);
            test.service().Update(test.updated());
            Assert.True(test.db().Get(expected.ID).Equals(test.updated()));
            Assert.False(test.db().Get(expected.ID).Equals(test.applicant()));
        }
    }
}
