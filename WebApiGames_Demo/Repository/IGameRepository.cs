using System.Collections.Generic;
using WebApiGames_Demo.Models;

namespace WebApiGames_Demo.Repository
{
    public interface IGameRepository : IRepository<Game>
    {
        IEnumerable<Game> GetGameByScore();
    }
}
