using System;
using System.Collections.Generic;
using Hahn.ApplicatonProcess.May2020.Data;

namespace Hahn.ApplicatonProcess.May2020.Domain
{
    public interface IApplicantValidator
    {
        bool Validate(Applicant applicant, out IEnumerable<string> errors);
    }
}
