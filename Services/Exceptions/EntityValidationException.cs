using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.Exceptions
{
	/// <summary>
	/// Respresents validation errors caused by end users
	/// </summary>
	public class EntityValidationException : ApplicationException
	{
		public IDictionary<string, string[]> Errors { get; private set; }

		public EntityValidationException(string message, IDictionary<string, string[]> errors) : base(message)
		{
			Errors = errors;
		}

        public EntityValidationException(string message, IDictionary<string, string[]> errors,
			Exception innerException) : base(message, innerException)
        {
			Errors = errors;
        }
    }
}
