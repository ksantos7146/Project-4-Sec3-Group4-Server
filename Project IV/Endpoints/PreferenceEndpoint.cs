using Project_IV.Dtos;
using Project_IV.Entities;
using Project_IV.Mappers;
using Project_IV.Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IV.Endpoints
{
    public class PreferenceEndpoint
    {
        private readonly IPreferenceService _preferenceService;

        public PreferenceEndpoint(IPreferenceService preferenceService)
        {
            _preferenceService = preferenceService;
        }

        public async Task<IEnumerable<PreferenceDto>> GetAllPreferences()
        {
            var preferences = await _preferenceService.GetAllPreferencesAsync();
            return preferences.Select(p => p.ToDto());
        }

        public async Task<PreferenceDto> GetPreferenceByUserId(string userId)
        {
            var preference = await _preferenceService.GetPreferenceByUserIdAsync(userId);
            return preference?.ToDto();
        }

        public async Task<PreferenceDto> CreatePreference(PreferenceDto preferenceDto)
        {
            var preference = preferenceDto.ToEntity();
            await _preferenceService.AddPreferenceAsync(preference);
            return preference.ToDto();
        }

        public async Task<PreferenceDto> UpdatePreference(string userId, PreferenceDto preferenceDto)
        {
            var existingPreference = await _preferenceService.GetPreferenceByUserIdAsync(userId);
            if (existingPreference == null)
            {
                return null;
            }

            var updatedPreference = preferenceDto.ToEntity();
            updatedPreference.PreferenceId = existingPreference.PreferenceId;
            updatedPreference.UserId = userId;

            await _preferenceService.UpdatePreferenceAsync(updatedPreference);
            return updatedPreference.ToDto();
        }

        public async Task<bool> DeletePreference(string userId)
        {
            var preference = await _preferenceService.GetPreferenceByUserIdAsync(userId);
            if (preference == null)
            {
                return false;
            }

            await _preferenceService.DeletePreferenceAsync(preference.PreferenceId);
            return true;
        }
    }
}
