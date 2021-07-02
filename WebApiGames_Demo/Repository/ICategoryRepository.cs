using System.Collections.Generic;
using WebApiGames_Demo.Models;

namespace WebApiGames_Demo.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> GetCategoriesGames();
    }
}
