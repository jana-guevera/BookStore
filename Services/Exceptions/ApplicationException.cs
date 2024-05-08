using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Exceptions
{
	/// <summary>
	/// Represents all errors caused by end users
	/// </summary>
	public class ApplicationException : Exception
	{
		public ApplicationException(string message) : base(message)
		{

		}
	}
}
