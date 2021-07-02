using System;
using System.Collections.Generic;
using System.Linq;
using WebApiGames_Demo.Context;
using WebApiGames_Demo.Models;

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
    }
}
