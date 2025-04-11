using Microsoft.EntityFrameworkCore;
using Project_IV.Data;
using Project_IV.Entities;

namespace Project_IV.Service.Impl
{
    public class PreferenceService : IPreferenceService
    {
        private readonly GitCommitDbContext _dbContext;

        public PreferenceService(GitCommitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Preference> GetPreferenceByUserIdAsync(string userId) =>
            await _dbContext.Preferences.FirstOrDefaultAsync(p => p.UserId == userId);

        public async Task<IEnumerable<Preference>> GetAllPreferencesAsync() =>
            await _dbContext.Preferences.ToListAsync();

        public async Task AddPreferenceAsync(Preference preference)
        {
            await _dbContext.Preferences.AddAsync(preference);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePreferenceAsync(Preference preference)
        {
            var existingPreference = await _dbContext.Preferences
                .FirstOrDefaultAsync(p => p.UserId == preference.UserId);

            if (existingPreference == null)
            {
                // If no preference exists, create a new one
                await _dbContext.Preferences.AddAsync(preference);
            }
            else
            {
                // Update existing preference
                existingPreference.MinAge = preference.MinAge;
                existingPreference.MaxAge = preference.MaxAge;
                existingPreference.GenderId = preference.GenderId;
                existingPreference.StateId = preference.StateId;
                _dbContext.Preferences.Update(existingPreference);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePreferenceAsync(int id)
        {
            var preference = await _dbContext.Preferences.FindAsync(id);
            if (preference != null)
            {
                _dbContext.Preferences.Remove(preference);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
