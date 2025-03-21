using Project_IV.Dtos;
using Project_IV.Service.Impl;
using Project_IV.Mappers;
using System.Threading.Tasks;
using Project_IV.Service;

namespace Project_IV.Endpoints
{
    public class PreferenceEndpoint
    {
        private readonly IPreferenceService _preferenceService;

        public PreferenceEndpoint(IPreferenceService preferenceService)
        {
            _preferenceService = preferenceService;
        }

        // Get a preference by ID
        public async Task<PreferenceDto> GetPreferenceById(int id)
        {
            var preference = await _preferenceService.GetPreferenceByUserIdAsync(id);
            return preference?.ToDto();
        }

        // Create a new preference
        public async Task<PreferenceDto> CreatePreference(PreferenceDto preferenceDto)
        {
            var preference = preferenceDto.ToEntity(); // Map DTO to entity
            await _preferenceService.AddPreferenceAsync(preference);
            return preference.ToDto(); // Return the created preference as DTO
        }

        // Update a preference
        public async Task<PreferenceDto> UpdatePreference(int id, PreferenceDto preferenceDto)
        {
            var preference = await _preferenceService.GetPreferenceByUserIdAsync(id);
            if (preference == null) return null;

            preference = preferenceDto.ToEntity(); // Map DTO to entity
            await _preferenceService.UpdatePreferenceAsync(preference);
            return preference.ToDto(); // Return the updated preference as DTO
        }

        // Delete a preference
        public async Task<bool> DeletePreference(int id)
        {
            var preference = await _preferenceService.GetPreferenceByUserIdAsync(id);
            if (preference == null) return false;

            await _preferenceService.DeletePreferenceAsync(id);
            return true;
        }
    }
}
