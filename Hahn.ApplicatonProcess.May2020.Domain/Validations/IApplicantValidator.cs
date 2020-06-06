using System.Collections.Generic;

namespace Data.Validations
{
    public interface IApplicantValidator
    {
        bool Validate(Applicant applicant, out IEnumerable<string> errors);
    }
}
