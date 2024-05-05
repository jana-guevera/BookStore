using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ErrorHandling
{
    /// <summary>
    /// Represents errors which occur when required data is not provided
    /// </summary>
    public class NoArgumentError : ApplicationError
    {
        public NoArgumentError(string message) : base(message)
        {
            
        }
    }
}
