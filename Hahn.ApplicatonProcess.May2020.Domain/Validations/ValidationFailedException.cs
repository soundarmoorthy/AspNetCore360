using System;
using System.Collections.Generic;

namespace Hahn.ApplicatonProcess.May2020.Domain
{
    public sealed class ValidationFailedException : Exception
    {
        private readonly IEnumerable<string> errors;

        public ValidationFailedException(IEnumerable<string> errors)
        {
            this.errors = errors;
        }

        public override string Message =>
            string.Join(Environment.NewLine, "Validation failed : ", errors);
    }
}
