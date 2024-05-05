using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ErrorHandling
{
    public class UniqueValidationException : ApplicationException
    {
        public UniqueValidationException(string message) : base(message)
        {
        }
    }
}
