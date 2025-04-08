using Project_IV.Data;
using Project_IV.Entities;
using Microsoft.EntityFrameworkCore;

namespace Project_IV.Service.Impl
{
    public class UserService : IUserService
    {
        private readonly GitCommitDbContext _dbContext;

        public UserService(GitCommitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserByIdAsync(string id) =>
            await _dbContext.Users.Include(u => u.Images).FirstOrDefaultAsync(u => u.Id == id);

        public async Task<IEnumerable<User>> GetAllUsersAsync() =>
            await _dbContext.Users.Include(u => u.Images).ToListAsync();

        public async Task AddUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateUserStateAsync(string userId, int stateId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user != null)
            {
                user.StateId = stateId;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
