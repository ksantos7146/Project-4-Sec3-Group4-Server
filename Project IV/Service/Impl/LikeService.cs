using Project_IV.Data;
using Project_IV.Entities;
using Microsoft.EntityFrameworkCore;

namespace Project_IV.Service.Impl
{
    public class LikeService : ILikeService
    {
        private readonly GitCommitDbContext _dbContext;

        public LikeService(GitCommitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Like> GetLikeByIdAsync(int id) =>
            await _dbContext.Likes.FindAsync(id);

        public async Task<IEnumerable<Like>> GetAllLikesAsync() =>
            await _dbContext.Likes.ToListAsync();

        public async Task AddLikeAsync(Like like)
        {
            await _dbContext.Likes.AddAsync(like);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveLikeAsync(int id)
        {
            var like = await GetLikeByIdAsync(id);
            if (like != null)
            {
                _dbContext.Likes.Remove(like);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
