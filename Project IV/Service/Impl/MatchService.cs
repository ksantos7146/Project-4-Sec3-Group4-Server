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
            await _dbContext.Matches.FindAsync(id);

        public async Task<IEnumerable<Match>> GetAllMatchesAsync() =>
            await _dbContext.Matches.ToListAsync();

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
