using Project_IV.Entities;

namespace Project_IV.Service
{
    public interface IPreferenceService
    {
        Task<Preference> GetPreferenceByUserIdAsync(string userId);
        Task<IEnumerable<Preference>> GetAllPreferencesAsync();
        Task AddPreferenceAsync(Preference preference);
        Task UpdatePreferenceAsync(Preference preference);
        Task DeletePreferenceAsync(int id);
    }
}
