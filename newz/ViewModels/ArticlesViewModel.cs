using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Fusillade;
using newz.DTOs;
using newz.Services;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace newz.ViewModels
{
	public class ArticlesViewModel : BaseViewModel
	{
		public ObservableCollection<Article> Articles = new ObservableCollection<Article>();

		public ICommand GetArticlesCommand => new Command(async () => {
			if (CrossConnectivity.Current.IsConnected)
			{
				await GetArticles(Priority.UserInitiated);
			}
			else
			{
				await Utils.Alerts.NoConnection();
			}
			 
		});

		private IService _service;

		public ArticlesViewModel(IService service)
		{
			_service = service;
		}

		public async Task GetArticles(Priority priority)
		{
			var articles = await _service.GetArticles(priority);
			if (articles != null && articles.Count > 0)
			{
				Articles.Clear();
				foreach (var article in articles)
				{
					Articles.Add(article);
				}
			}
			else
			{
				Utils.Alerts.Show("No Results", "There aren't any news to display, please try again later", "Ok");
			}
		}
	}
}
