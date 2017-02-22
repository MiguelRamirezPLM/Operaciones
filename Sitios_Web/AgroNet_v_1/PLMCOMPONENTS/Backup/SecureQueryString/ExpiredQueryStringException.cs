using System;

namespace PLMComponents
{
	/// <summary>
	/// Thrown when a queryString has expired and is therefore no longer valid.
	/// </summary>
	public class ExpiredQueryStringException : System.Exception
	{
		public ExpiredQueryStringException() : base() {}
	}
}
