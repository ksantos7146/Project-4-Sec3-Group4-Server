using Project_IV.Entities;

namespace Project_IV.Service
{
    public interface IPreferenceService
    {
        Task<Preference> GetPreferenceByUserIdAsync(int userId);
        Task AddPreferenceAsync(Preference preference);
        Task UpdatePreferenceAsync(Preference preference);
        Task DeletePreferenceAsync(int preferenceId);
    }
}
