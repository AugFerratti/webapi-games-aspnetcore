using System.Collections.Generic;
using System.Linq;
using WebApiGames_Demo.Context;
using WebApiGames_Demo.Models;
using WebApiGames_Demo.Pagination;

namespace WebApiGames_Demo.Repository
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        public GameRepository(AppDbContext context) : base(context)
        {

        }
        public IEnumerable<Game> GetGameByScore()
        {
            return Get().OrderBy(g => g.Score).ToList();
        }

        public PagedList<Game> GetGames(GamesParameters gamesParameters)
        {
            //return Get()
            //    .OrderBy(on => on.Name)
            //    .Skip((gamesParameters.PageNumber - 1) * gamesParameters.PageSize)
            //    .Take(gamesParameters.PageSize)
            //    .ToList();

            return PagedList<Game>.ToPagedList(Get().OrderBy(on => on.GameId),
                gamesParameters.PageNumber, gamesParameters.PageSize);
        }
    }
}
