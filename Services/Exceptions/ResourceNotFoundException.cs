using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Exceptions
{
    /// <summary>
    /// Represents errors raised by users when invalid record id is provided
    /// </summary>
    public class ResourceNotFoundException : ApplicationException
    {
        public ResourceNotFoundException(string message) : base(message) { }

        public ResourceNotFoundException(string message, Exception innerException) : 
            base(message, innerException) { }
    }
}
