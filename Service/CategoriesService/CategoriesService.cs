
using Dto;
using Repo;

namespace Service
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoriesService
            (
                ICategoryRepository categoryRepo
            )
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<List<DropDownDto>> GetCategoriesDDAsync()
        {
            var categories = await _categoryRepo.GetCategoriesAsync();
            return categories.Select(x => new DropDownDto { Text = x.CategoryName, Value = x.Id}).ToList();
        }
    }
}
