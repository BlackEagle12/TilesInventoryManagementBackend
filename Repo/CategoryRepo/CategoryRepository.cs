
using Core;
using Data.Contexts;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Repo
{
    public class CategoryRepository : BaseRepo<Category>, ICategoryRepository
    {
        public CategoryRepository
            (
                InventoryDBContext context
            ) : base(context)
        {

        }

        public async Task<Dictionary<int, Category>> GetCategoryDictAsync(List<int> categoryIdList)
        {
            return await
                       Select(x => categoryIdList.Contains(x.Id))
                       .ToDictionaryAsync(x => x.Id);
        }

        public async Task<Category> GetCategoryAsync(int categoryId)
        {
            return await GetByIdAsync(categoryId) ?? throw new ApiException(StatusCodes.Status404NotFound, "Category Not Found");
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await GetQueyable().ToListAsync();
        }
    }
}
