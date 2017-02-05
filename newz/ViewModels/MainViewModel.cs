using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Fusillade;
using newz.DTOs;
using newz.Services;
using newz.Views;
using Xamarin.Forms;

namespace newz.ViewModels
{
	public class MainViewModel : BaseViewModel
	{
		public ICommand GoToRssNewsCommand => new Command(async () => { await Navigation.PushAsync(new ArticlesView(new RssService())); });
		public ICommand GoToApiNewsCommand => new Command(async () => { await Navigation.PushAsync(new ArticlesView(new ApiService())); });
	}
}
