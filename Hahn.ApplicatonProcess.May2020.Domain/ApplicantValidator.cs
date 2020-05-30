using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Validators;
using Hahn.ApplicatonProcess.May2020.Data;

namespace Hahn.ApplicatonProcess.May2020.Domain
{
    public class ApplicantValidator : AbstractValidator<Applicant>,
        IApplicantValidator
    {
        public ApplicantValidator()
        {
            RuleFor(x => x.Name).MinimumLength(5);
            RuleFor(x => x.FamilyName).MinimumLength(5);
            RuleFor(x => x.Address).MinimumLength(10);
            RuleFor(x => x.Age).InclusiveBetween(20, 60);
            RuleFor(x => x.Hired).NotNull();
            RuleFor(x => x.EMailAdress).
                EmailAddress(EmailValidationMode.AspNetCoreCompatible);
        }

        public bool Validate(Applicant applicant,
            out IEnumerable<string> errors)
        {
            if (applicant == null)
                throw new ArgumentNullException(
                    $"{nameof(applicant)} cannot be null");

            var result = this.Validate(applicant);
            errors = result.Errors.Select(x => x.ErrorMessage);
            return result.IsValid;
        }
    }
}
