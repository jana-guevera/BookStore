using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ErrorHandling
{
    /// <summary>
    /// Respresents validation errors caused by end users
    /// </summary>
    public class ValidationError : ApplicationError
    {
        public Dictionary<string, string> Errors { get; set; }
    }
}
