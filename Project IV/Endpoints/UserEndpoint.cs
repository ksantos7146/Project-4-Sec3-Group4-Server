using Microsoft.EntityFrameworkCore;
using Project_IV.Data;
using Project_IV.Dtos;
using Project_IV.Entities;
using Project_IV.Mappers;

namespace Project_IV.Endpoints
{
    public class UserEndpoint
    {
        private readonly GitCommitDbContext _dbContext;

        public UserEndpoint(GitCommitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            return user?.ToDto();
        }

        public async Task<UserDto> CreateUser(UserDto userDto)
        {
            var user = userDto.ToEntity();
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user.ToDto();
        }

        public async Task<UserDto> UpdateUser(int id, UserDto userDto)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return null;
            }

            user.Username = userDto.Username;
            user.Bio = userDto.Bio;
            user.Gender = userDto.Gender;
            user.State = userDto.State;
            user.Age = userDto.Age;

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            return user.ToDto();
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ImageDto>> GetImagesByUserId(int userId)
        {
            var user = await _dbContext.Users
                .Where(u => u.UserId == userId)
                .Include(u => u.Images)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return new List<ImageDto>();
            }

            return user.Images.Select(i => i.ToDto()).ToList();
        }

        public async Task<IEnumerable<ImageDto>> CreateImagesByUserId(int userId, IEnumerable<ImageDto> imageDtos)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return new List<ImageDto>();
            }

            var images = imageDtos.Select(dto => new Image
            {
                ImageData = dto.ImageData,
                UploadedAt = dto.UploadedAt,
                UserId = userId
            }).ToList();

            await _dbContext.Images.AddRangeAsync(images);
            await _dbContext.SaveChangesAsync();

            return images.Select(i => i.ToDto()).ToList();
        }
    }
}
