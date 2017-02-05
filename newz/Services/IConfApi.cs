using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using newz.DTOs;
using Refit;

namespace newz.Services
{
	[Headers("Accept: application/json")]
	public interface IConfApi
	{
		[Get("/articles?source=bbc-news&sortBy=top&apiKey={apiKey}")]
		Task<ArticleResponse> GetArticles(string apiKey);
	}
}
