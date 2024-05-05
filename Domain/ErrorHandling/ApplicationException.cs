using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ErrorHandling
{
    /// <summary>
    /// Represents all errors caused by end users
    /// </summary>
    public class ApplicationException : Exception
    {
        public ApplicationException(string message): base(message)
        {
            
        }
    }
}
