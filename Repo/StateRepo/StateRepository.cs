
using Core;
using Data.Contexts;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Repo
{
    public class StateRepository : BaseRepo<State>, IStateRepository
    {
        public StateRepository
            (
                InventoryDBContext context
            ) : base(context)
        {

        }

        public async Task<List<State>> GetAllStateAsync()
        {
            return await GetQueyable().ToListAsync();
        }

        public async Task<List<State>> GetAllStatesByCountryIdAsync(int countryId)
        {
            return await Select(x => x.CountryId == countryId).ToListAsync();
        }

        public async Task<State> GetStateAsync(int stateId)
        {
            return await GetByIdAsync(stateId) ?? throw new ApiException(StatusCodes.Status404NotFound, "State Not Found");
        }

        public async Task<Dictionary<int, State>> GetStateDictAsync(List<int> stateIdList)
        {
            return await
                        Select(x => stateIdList.Contains(x.Id))
                        .ToDictionaryAsync(x => x.Id);
        }
    }
}
