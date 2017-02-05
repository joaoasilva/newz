using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace newz.Views
{
	public partial class ArticleDetailView : ContentPage
	{
		public ArticleDetailView(string url, string title)
		{
			InitializeComponent();
			Title = title;
			Browser.Source = url;
		}

		void webOnNavigating(object sender, WebNavigatingEventArgs e)
		{
			//HideBrowser();
			Loading.Hide();
		}

		void webOnEndNavigating(object sender, WebNavigatedEventArgs e)
		{
			//ShowBrowser();
			Loading.Show();
		}

		private void ShowBrowser()
		{
			
			Browser.Opacity = 0;
			Browser.IsVisible = true;
			Browser.FadeTo(1, Constants.FADDING_TIME);
		}

		private void HideBrowser()
		{
			Browser.FadeTo(0, Constants.FADDING_TIME);
			Browser.IsVisible = false;

		}
	}
}
