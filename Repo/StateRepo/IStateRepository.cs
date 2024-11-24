
using Data.Models;

namespace Repo
{
    public interface IStateRepository : IBaseRepo<State>
    {
        Task<List<State>> GetAllStateAsync();
        Task<List<State>> GetAllStatesByCountryIdAsync(int countryId);
        Task<State> GetStateAsync(int stateId);
        Task<Dictionary<int, State>> GetStateDictAsync(List<int> stateIdList);
    }
}
