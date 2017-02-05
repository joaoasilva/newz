using System;
using System.Collections.Generic;
using Fusillade;
using newz.DTOs;
using newz.Services;
using newz.ViewModels;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace newz.Views
{
	public partial class ArticlesView : ContentPage
	{
		private readonly ArticlesViewModel _viewModel;

		public ArticlesView(IService service)
		{
			InitializeComponent();

			Title = "BBC News";

			_viewModel = new ArticlesViewModel(service);
			_viewModel.Navigation = Navigation;

			BindingContext = _viewModel;

			ArticlesList.ItemsSource = _viewModel.Articles;
			ArticlesList.ItemSelected += ArticlesList_ItemSelected;
			ArticlesList.ItemTapped += ArticlesList_ItemTapped;

			var refresh = new ToolbarItem
			{
				Command = _viewModel.GetArticlesCommand,
				Icon = "refresh.png",
				Text = "Refresh",
				Priority = 0
			};

			ToolbarItems.Add(refresh);
			GetArticles();
		}

		private async void GetArticles()
		{
			Loading.Show();
			await _viewModel.GetArticles(Priority.Explicit);
			Loading.Hide();
		}

		void ArticlesList_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e == null) return;
			((ListView)sender).SelectedItem = null;
		}

		void ArticlesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var article = (Article)e.SelectedItem;
			if (article == null) return;
			if (CrossConnectivity.Current.IsConnected)
			{
				Navigation.PushAsync(new ArticleDetailView(article.url, article.title));
			}
			else 
			{
				Utils.Alerts.NoConnection();
			}
		}
	}
}
