using System;
using Xamarin.Forms;

namespace newz.Utils
{
	public class Loading : ActivityIndicator
	{
		public Loading()
		{
			VerticalOptions = LayoutOptions.FillAndExpand;
			HorizontalOptions = LayoutOptions.FillAndExpand;
		}

		public void Show()
		{
			Opacity = 0;
			IsVisible = true;
			this.FadeTo(1, Constants.FADDING_TIME);
		}

		public void Hide()
		{
			this.FadeTo(0, Constants.FADDING_TIME);
			IsVisible = false;
		}
	}
}
