using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApiGames_Demo.Context;
using WebApiGames_Demo.Models;
using WebApiGames_Demo.Pagination;

namespace WebApiGames_Demo.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {

        }

        public PagedList<Category> GetCategories(CategoriesParameters categoryParameters)
        {
            return PagedList<Category>.ToPagedList(Get().OrderBy(on => on.Name),
                categoryParameters.PageNumber,
                categoryParameters.PageSize);
        }
        public IEnumerable<Category> GetCategoriesGames()
        {
            return Get().Include(x => x.Games);
        }
    }
}
