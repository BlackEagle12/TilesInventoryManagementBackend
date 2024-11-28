using Dto;

namespace Service
{
    public interface ICategoriesService
    {
        Task<List<DropDownDto>> GetCategoriesDDAsync();
    }
}
