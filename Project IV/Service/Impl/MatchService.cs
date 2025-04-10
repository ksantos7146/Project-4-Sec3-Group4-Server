using Project_IV.Data;
using Microsoft.EntityFrameworkCore;
using Project_IV.Entities;

namespace Project_IV.Service.Impl
{
    public class MatchService : IMatchService
    {
        private readonly GitCommitDbContext _dbContext;

        public MatchService(GitCommitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Match> GetMatchByIdAsync(int id) =>
            await _dbContext.Matches
                .Include(m => m.User1)
                .Include(m => m.User2)
                .FirstOrDefaultAsync(m => m.MatchId == id);

        public async Task<IEnumerable<Match>> GetAllMatchesAsync() =>
            await _dbContext.Matches
                .Include(m => m.User1)
                    .ThenInclude(u => u.Images)
                .Include(m => m.User2)
                    .ThenInclude(u => u.Images)
                .ToListAsync();

        public async Task AddMatchAsync(Match match)
        {
            await _dbContext.Matches.AddAsync(match);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveMatchAsync(int id)
        {
            var match = await GetMatchByIdAsync(id);
            if (match != null)
            {
                _dbContext.Matches.Remove(match);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
