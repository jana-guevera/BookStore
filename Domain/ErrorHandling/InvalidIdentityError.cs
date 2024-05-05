using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ErrorHandling
{
    /// <summary>
    /// Represents errors which occur when invalid indentity is provided
    /// </summary>
    public class InvalidIdentityError : ApplicationError
    {
        public InvalidIdentityError(string message) : base(message)
        {
            
        }
    }
}
