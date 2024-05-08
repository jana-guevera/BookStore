using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Exceptions
{
	/// <summary>
	/// Represents errors which occur when required data is not provided
	/// </summary>
	public class NullArgumentException : ApplicationException
	{
		public NullArgumentException(string message) : base(message)
		{

		}
	}
}
