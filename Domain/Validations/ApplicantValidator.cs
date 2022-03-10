
using Data;
using System;
using System.Linq;
using Data.Validations;
using FluentValidation;
using System.Collections.Generic;
using FluentValidation.Validators;

namespace Domain.Validations
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

            //RuleFor(x => x.CountryOfOrigin).IsValidCountry()
               // .WithMessage("The country name is invalid");
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
