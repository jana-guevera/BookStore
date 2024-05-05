using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.ErrorHandling
{
    /// <summary>
    /// Respresents validation errors caused by end users
    /// </summary>
    public class ValidationError : ApplicationError
    {
        public List<ValidationResult> Errors { get; set; }

        public ValidationError(string message, List<ValidationResult> errors) : base(message)
        {
            Errors = errors;
        }
    }
}
