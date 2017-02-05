using System;
using System.Diagnostics;

namespace newz.Utils
{
	public static class AsyncErrorHandler
	{
		public static void HandleException(Exception exception)
		{
			Debug.WriteLine(exception.Message);
		}
	}
}
