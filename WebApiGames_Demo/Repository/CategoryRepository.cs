using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApiGames_Demo.Context;
using WebApiGames_Demo.Models;

namespace WebApiGames_Demo.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {

        }
        public IEnumerable<Category> GetCategoriesGames()
        {
            return Get().Include(x => x.Games);
        }
    }
}
