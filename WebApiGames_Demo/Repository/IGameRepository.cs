using System.Collections.Generic;
using WebApiGames_Demo.Models;
using WebApiGames_Demo.Pagination;

namespace WebApiGames_Demo.Repository
{
    public interface IGameRepository : IRepository<Game>
    {
        PagedList<Game> GetGames(GamesParameters gamesParameters);
        IEnumerable<Game> GetGameByScore();
    }
}
