using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<Game>> GetGameByScore()
        {
            return await Get().OrderBy(g => g.Score).ToListAsync();
        }

        public async Task<PagedList<Game>> GetGames(GamesParameters gamesParameters)
        {
            return await PagedList<Game>.ToPagedList(Get().OrderBy(on => on.GameId),
                gamesParameters.PageNumber, gamesParameters.PageSize);
        }
    }
}
