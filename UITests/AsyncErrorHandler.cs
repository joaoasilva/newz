using System;
using System.Diagnostics;

namespace newz.UITests
{
	public class AsyncErrorHandler
	{
		public static void HandleException(Exception exception)
		{
			Debug.WriteLine(exception.Message);
		}
	}
}
