using System.Threading.Tasks;

namespace WebApiGames_Demo.Repository
{
    public interface IUnitOfWork
    {
        IGameRepository GameRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        Task Commit();
    }
}
