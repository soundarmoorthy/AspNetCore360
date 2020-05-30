using System;
using System.Linq;
using System.Collections.Generic;
using Hahn.ApplicatonProcess.May2020.Data;

namespace Hahn.ApplicatonProcess.May2020.Domain
{
    public sealed class ApplicantService : IApplicantService
    {
        private readonly IApplicantValidator validator;
        private readonly IApplicantRepository repo;
        public ApplicantService(IApplicantValidator validator,
            IApplicantRepository repo)
        {
            this.validator = validator;
            this.repo = repo;
        }

        public void Create(Applicant applicant)
        {
            if (applicant == null)
                throw new ValidationFailedException
                    (new[] { $"{nameof(applicant)} is null or empty" });

            if (!validator.Validate(applicant, out IEnumerable<string> errors))
                throw new ValidationFailedException(errors);

            var present = repo.Get(applicant.ID) != null;

            if (present)
                throw new ApplicantAlreadyExistsException(applicant);

            repo.Add(applicant);

        }

        public void Delete(int ID)
        {
            var applicant = repo.Get(ID);

            if (applicant == null)
                throw new ApplicantMissingExcpetion(ID);

            repo.Remove(applicant);
        }

        public Applicant Get(int ID)
        {
            var applicant = repo.Get(ID);

            if (applicant == null)
                throw new ApplicantMissingExcpetion(ID);

            return applicant;
        }

        public void Update(Applicant dest)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            var source = repo.Get(dest.ID);

            if (source == null)
                throw new ApplicantMissingExcpetion(dest.ID);

            if (!validator.Validate(dest, out IEnumerable<string> errors))
                throw new ValidationFailedException(errors);

            repo.Update(dest);
        }
    }
}
