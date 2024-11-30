

using Dto;

namespace Service
{
    public interface IRolesService
    {
        Task<List<DropDownDto>> GetAllRolesDDAsync();
        Task<List<DropDownDto>> GetDefaultRolesDDAsync();
    }
}
