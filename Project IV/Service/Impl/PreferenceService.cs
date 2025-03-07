using Project_IV.Data;
using Project_IV.Entities;
using Microsoft.EntityFrameworkCore;

namespace Project_IV.Service.Impl
{
    public class PreferenceService : IPreferenceService
    {
        private readonly GitCommitDbContext _dbContext;

        public PreferenceService(GitCommitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Preference> GetPreferenceByUserIdAsync(int userId) =>
            await _dbContext.Preferences.FirstOrDefaultAsync(p => p.UserId == userId);

        public async Task AddPreferenceAsync(Preference preference)
        {
            await _dbContext.Preferences.AddAsync(preference);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePreferenceAsync(Preference preference)
        {
            _dbContext.Preferences.Update(preference);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePreferenceAsync(int preferenceId)
        {
            var preference = await _dbContext.Preferences.FindAsync(preferenceId);
            if (preference != null)
            {
                _dbContext.Preferences.Remove(preference);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
