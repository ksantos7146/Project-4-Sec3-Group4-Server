using Project_IV.Entities;

namespace Project_IV.Service
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(string id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task UpdateUserStateAsync(string userId, int stateId);
    }
}
