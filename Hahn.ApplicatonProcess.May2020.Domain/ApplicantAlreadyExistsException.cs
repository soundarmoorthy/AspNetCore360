using Data;
using System;

namespace Domain
{
    public sealed class ApplicantAlreadyExistsException : Exception
    {
        private readonly Applicant applicant;
        public ApplicantAlreadyExistsException(Applicant applicant)
        {
            this.applicant = applicant;
        }

        public override string Message =>
            $"The applicant with {applicant.ID} already exists";
    }
}
