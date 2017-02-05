using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Akavache;
using Fusillade;
using newz.DTOs;
using newz.Utils;

namespace newz.Services
{
	public class RssService : IService
	{
		public async Task<List<Article>> GetArticles(Priority priority)
		{
			var cache = BlobCache.LocalMachine;

			if (priority == Priority.UserInitiated)
			{
				await cache.Invalidate(Cache.ArticlesResponseKey);
			}

			var cachedArticles = cache.GetOrFetchObject(Cache.RssArticlesKey, () => GetRssFeedArticles());
			var articles = await cachedArticles.FirstOrDefaultAsync();

			return articles;
		}

		private async Task<List<Article>> GetRssFeedArticles()
		{
			var articles = new List<Article>();
			var httpClient = new HttpClient();
			var feed = Constants.RSS_ADDRESS;
			var responseString = await httpClient.GetStringAsync(feed);

			var items = await ParseRssFeed(responseString);
			foreach (var item in items)
			{
				articles.Add(item);
			}
			return articles;
		}

		private async Task<List<Article>> ParseRssFeed(string rss)
		{
			return await Task.Run(() =>
			{
				var xdoc = XDocument.Parse(rss);
				var media = XNamespace.Get(Constants.RSS_MEDIA_ADDRESS);

				var rssItems = (
					from item in xdoc.Descendants("item")
					select new Article
				{
					title = (string)item.Element("title"),
					description = (string)item.Element("description"),
					url = (string)item.Element("link"),
					publishedAt = (string)item.Element("pubDate"),
					urlToImage = item.Element(media + "thumbnail") != null ? item.Element(media + "thumbnail").Attribute("url").Value : ""
				}).ToList();
				return rssItems.ToList();
			});
		}
	}
}
