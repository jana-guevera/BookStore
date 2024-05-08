using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Exceptions
{
	/// <summary>
	/// Represents errors caused by dublicate record 
	/// </summary>
	public class UniqueValidationException : ApplicationException
	{
		public UniqueValidationException(string message) : base(message)
		{
		}
	}
}
