using Project_IV.Dtos;
using Project_IV.Entities;
using Project_IV.Mappers;
using Project_IV.Service;
using System.Collections.Generic;

namespace Project_IV.Endpoints
{
    public class UserEndpoint
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UserEndpoint(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return users.Select(u => u.ToDto());
        }

        public async Task<UserDto> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return user?.ToDto();
        }

        public async Task<UserDto> CreateUser(UserDto userDto)
        {
            var user = userDto.ToEntity();
            await _userService.AddUserAsync(user);
            return user.ToDto();
        }

        public async Task<UserDto> UpdateUser(UserDto userDto)
        {
            var userId = await _authService.GetCurrentUserIdAsync();
            var user = await _userService.GetUserByIdAsync(userId); 
            if (user == null)
            {
                return null;
            }

            user.UserName = userDto.Username;
            user.Bio = userDto.Bio;
            user.GenderId = userDto.GenderId;
            user.StateId = userDto.StateId;
            user.Age = userDto.Age;

            await _userService.UpdateUserAsync(user);
            return user.ToDto();
        }

        public async Task<bool> DeleteUser(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            await _userService.DeleteUserAsync(user);
            return true;
        }

        public async Task<IEnumerable<ImageDto>> GetImagesByUserId(string userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return Enumerable.Empty<ImageDto>();
            }

            return user.Images.Select(i => i.ToDto());
        }

        public async Task<IEnumerable<ImageDto>> CreateImagesForUser(string userId, IEnumerable<ImageDto> imageDtos)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return Enumerable.Empty<ImageDto>();
            }

            var images = imageDtos.Select(dto => dto.ToEntity());
            foreach (var image in images)
            {
                user.Images.Add(image);
            }

            await _userService.UpdateUserAsync(user);
            return user.Images.Select(i => i.ToDto());
        }
    }
}
