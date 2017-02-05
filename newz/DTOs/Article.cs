using System;

namespace newz.DTOs
{
	public class Article
	{
		public string author { get; set; }
		public string title { get; set; }
		public string description { get; set; }
		public string url { get; set; }
		public string urlToImage { get; set; }
		public string publishedAt { get; set; }

		public string date
		{
			get
			{
				var pubDate = DateTime.Now;
				if (!string.IsNullOrEmpty(publishedAt)) pubDate = DateTime.Parse(publishedAt).ToUniversalTime();
				var today = DateTime.UtcNow;
				var displayDate = "";
				if (pubDate.Day == today.Day && pubDate.Month == today.Month && pubDate.Year == today.Year)
				{
					displayDate = "Today";
				}
				else
				{
					displayDate = today.Day.ToString().PadLeft(2, '0') + "/" + today.Month.ToString().PadLeft(2, '0');
				}
				return displayDate + " " + pubDate.Hour.ToString().PadLeft(2, '0') + ":" + pubDate.Minute.ToString().PadLeft(2, '0'); ;
			}
		}
	}
}
