using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiGames_Demo.Models;
using WebApiGames_Demo.Pagination;

namespace WebApiGames_Demo.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<PagedList<Category>> GetCategories(CategoriesParameters categoriesParameters);
        Task<IEnumerable<Category>> GetCategoriesGames();
    }
}
