using System;
using System.Collections.Generic;
using System.Windows.Input;
using newz.Services;
using newz.ViewModels;
using Xamarin.Forms;

namespace newz.Views
{
	public partial class MainView : ContentPage
	{
		private readonly MainViewModel _viewModel;

		public MainView()
		{
			InitializeComponent();

			_viewModel = new MainViewModel();
			_viewModel.Navigation = Navigation;

			BindingContext = _viewModel;

			NavigationPage.SetHasNavigationBar(this, false);
		}
	}
}
