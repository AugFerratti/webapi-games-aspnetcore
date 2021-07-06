using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiGames_Demo.Models;
using WebApiGames_Demo.Pagination;

namespace WebApiGames_Demo.Repository
{
    public interface IGameRepository : IRepository<Game>
    {
        Task<PagedList<Game>> GetGames(GamesParameters gamesParameters);
        Task<IEnumerable<Game>> GetGameByScore();
    }
}
