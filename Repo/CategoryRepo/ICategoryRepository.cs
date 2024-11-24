
using Data.Models;

namespace Repo
{
    public interface ICategoryRepository : IBaseRepo<Category>
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryAsync(int categoryId);
        Task<Dictionary<int, Category>> GetCategoryDictAsync(List<int> categoryIdList);
    }
}
