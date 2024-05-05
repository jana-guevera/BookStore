using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ErrorHandling
{
    public class UniqueValidationError : ApplicationError
    {
        public UniqueValidationError(string message) : base(message)
        {
        }
    }
}
