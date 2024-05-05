using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.ErrorHandling
{
    /// <summary>
    /// Respresents validation errors caused by end users
    /// </summary>
    public class ModelValidationException : ApplicationException
    {
        public List<ValidationResult> Errors { get; set; }

        public ModelValidationException(string message, List<ValidationResult> errors) : base(message)
        {
            Errors = errors;
        }
    }
}
