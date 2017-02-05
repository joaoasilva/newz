using System;
using System.Collections.Generic;
using System.Net;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using Fusillade;
using newz.DTOs;
using newz.Utils;
using Plugin.Connectivity;
using Polly;

namespace newz.Services
{
	public class ApiService : IService
	{
		private readonly IApiService _apiService;
		private readonly string _apiKey = Constants.API_KEY;

		public ApiService()
		{
			_apiService = new ApiClient();
		}

		public async Task<List<Article>> GetArticles(Priority priority)
		{
			var cache = BlobCache.LocalMachine;

			if (priority == Priority.UserInitiated)
			{
				await cache.Invalidate(Cache.ArticlesResponseKey);
			}
				
			var cachedArticles = cache.GetOrFetchObject(Cache.ArticlesResponseKey, () => GetRemoteArticlesResponseAsync(priority));
			var articlesResponse = await cachedArticles.FirstOrDefaultAsync();

			return articlesResponse?.articles;
		}

		private async Task<ArticleResponse> GetRemoteArticlesResponseAsync(Priority priority)
		{
			if (string.IsNullOrEmpty(_apiKey)) {
				Utils.Alerts.Show("No Api Key", "Missing API key from https://newsapi.org/", "Ok");
				return new ArticleResponse();
			}

			ArticleResponse articles = null;
			Task<ArticleResponse> getArticlesTask;
			switch (priority)
			{
				case Priority.Background:
					getArticlesTask = _apiService.Background.GetArticles(_apiKey);
					break;
				case Priority.UserInitiated:
					getArticlesTask = _apiService.UserInitiated.GetArticles(_apiKey);
					break;
				case Priority.Speculative:
					getArticlesTask = _apiService.Speculative.GetArticles(_apiKey);
					break;
				default:
					getArticlesTask = _apiService.UserInitiated.GetArticles(_apiKey);
					break;
			}

			if (CrossConnectivity.Current.IsConnected)
			{
				articles = await Policy
					  .Handle<WebException>()
					  .WaitAndRetryAsync
					  (
						retryCount: 5,
						sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
					  )
					  .ExecuteAsync(async () => await getArticlesTask);
			}
			return articles;
		}
	}
}
