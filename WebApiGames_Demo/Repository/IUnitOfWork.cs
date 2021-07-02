namespace WebApiGames_Demo.Repository
{
    public interface IUnitOfWork
    {
        IGameRepository GameRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        void Commit();
    }
}
