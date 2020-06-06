using System;
using System.Collections.Generic;

namespace Data.Validations
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
