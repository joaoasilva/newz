using System.Collections.Generic;
using System.Threading.Tasks;
using Fusillade;
using newz.DTOs;

namespace newz.Services
{
	public interface IService
	{
		Task<List<Article>> GetArticles(Priority priority);
	}
}
