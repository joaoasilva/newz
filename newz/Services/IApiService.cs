using System;
namespace newz.Services
{
	public interface IApiService
	{
		IConfApi Speculative { get; }
		IConfApi UserInitiated { get; }
		IConfApi Background { get; }
	}
}
