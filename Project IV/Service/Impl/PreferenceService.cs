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
            _dbContext.Preferences.Update(preference);
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
