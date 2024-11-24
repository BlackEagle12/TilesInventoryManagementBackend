
using Dto;
using Repo;

namespace Service
{
    public class StateService : IStateService
    {
        private readonly IStateRepository _stateRepo;
        public StateService
            (
                IStateRepository stateRepo
            ) 
        {
            _stateRepo = stateRepo;
        }

        public async Task<List<DropDownDto>> GetStatesDDAsync()
        {
            var states = await _stateRepo.GetAllStateAsync();
            return states.Select(x => new DropDownDto() { Text = x.StateName, Value = x.Id }).ToList();
        }

        public async Task<List<DropDownDto>> GetStatesDDByCountryIdAsync(int countryId)
        {
            var states = await _stateRepo.GetAllStatesByCountryIdAsync(countryId);
            return states.Select(x => new DropDownDto() { Text = x.StateName, Value = x.Id }).ToList();
        }
    }
}
