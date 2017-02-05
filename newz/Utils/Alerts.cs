using System;
using System.Threading.Tasks;

namespace newz.Utils
{
	public static class Alerts
	{
		public static async Task Show(string title, string message, string cancel)
		{
			await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(title, message, cancel);
		}

		public static async Task NoConnection()
		{
			await Show("No connection", "Please try again later when you have internet connection", "Ok");
		}
	}
}
