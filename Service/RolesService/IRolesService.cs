

using Dto;

namespace Service
{
    public interface IRolesService
    {
        Task<List<DropDownDto>> GetDefaultRolesDDAsync();
    }
}
