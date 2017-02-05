using System;
using System.Net.Http;
using Fusillade;
using ModernHttpClient;
using Refit;

namespace newz.Services
{
	public class ApiClient : IApiService
	{
		private readonly Lazy<IConfApi> _background;
		private readonly Lazy<IConfApi> _userInitiated;
		private readonly Lazy<IConfApi> _speculative;

		public ApiClient()
		{
			Func<HttpMessageHandler, IConfApi> createClient = messageHandler =>
			{
				var client = new HttpClient(messageHandler)
				{
					BaseAddress = new Uri(Constants.API_ADDRESS)
				};

				return RestService.For<IConfApi>(client);
			};

			_background = new Lazy<IConfApi>(() => createClient(
				new RateLimitedHttpMessageHandler(new NativeMessageHandler(), Priority.Background)));

			_userInitiated = new Lazy<IConfApi>(() => createClient(
				new RateLimitedHttpMessageHandler(new NativeMessageHandler(), Priority.UserInitiated)));

			_speculative = new Lazy<IConfApi>(() => createClient(
				new RateLimitedHttpMessageHandler(new NativeMessageHandler(), Priority.Speculative)));
		}

		public IConfApi Background
		{
			get { return _background.Value; }
		}

		public IConfApi UserInitiated
		{
			get { return _userInitiated.Value; }
		}

		public IConfApi Speculative
		{
			get { return _speculative.Value; }
		}
	}
}
