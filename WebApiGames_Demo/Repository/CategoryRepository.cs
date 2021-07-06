using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<PagedList<Category>> GetCategories(CategoriesParameters categoryParameters)
        {
            return await PagedList<Category>.ToPagedList(Get().OrderBy(on => on.Name),
                categoryParameters.PageNumber,
                categoryParameters.PageSize);
        }
        public async Task<IEnumerable<Category>> GetCategoriesGames()
        {
            return await Get().Include(x => x.Games).ToListAsync();
        }
    }
}
