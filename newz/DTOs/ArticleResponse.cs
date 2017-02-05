using System;
using System.Collections.Generic;

namespace newz.DTOs
{
	public class ArticleResponse
	{
		public string status { get; set; }
		public string source { get; set; }
		public string sortBy { get; set; }
		public List<Article> articles { get; set; }
	}
}
