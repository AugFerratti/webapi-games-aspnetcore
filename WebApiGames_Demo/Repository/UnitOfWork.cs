using System.Threading.Tasks;
using WebApiGames_Demo.Context;

namespace WebApiGames_Demo.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private GameRepository _gameRepo;
        private CategoryRepository _categoryRepo;
        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IGameRepository GameRepository
        {
            get
            {
                return _gameRepo = _gameRepo ?? new GameRepository(_context);
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                return _categoryRepo = _categoryRepo ?? new CategoryRepository(_context);
            }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
