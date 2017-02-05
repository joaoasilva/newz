using System;
using PropertyChanged;
using Xamarin.Forms;

namespace newz.ViewModels
{
	[ImplementPropertyChanged]
	public class BaseViewModel
	{
		public INavigation Navigation;

		public BaseViewModel()
		{
		}
	}
}
