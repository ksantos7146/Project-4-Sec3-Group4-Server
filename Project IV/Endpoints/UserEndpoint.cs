using Project_IV.Dtos;
using Project_IV.Entities;
using Project_IV.Mappers;
using Project_IV.Service;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Project_IV.Endpoints
{
    public class UserEndpoint
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;

        public UserEndpoint(IUserService userService, IAuthService authService, UserManager<User> userManager)
        {
            _userService = userService;
            _authService = authService;
            _userManager = userManager;
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

        public async Task<UserDto> GetLoggedUser()
        {
            var userId = await _authService.GetCurrentUserIdAsync();
            if (userId == null)
            {
                return null;
            }

            var user = await _userService.GetUserByIdAsync(userId);
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
            if (userId == null)
            {
                return null;
            }

            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            // Update basic info
            user.UserName = userDto.Username;
            user.Email = userDto.Email;
            user.Bio = userDto.Bio;
            user.GenderId = userDto.GenderId;
            user.StateId = userDto.StateId;
            user.Age = userDto.Age;

            // Update password if provided
            if (!string.IsNullOrEmpty(userDto.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, userDto.Password);
                if (!result.Succeeded)
                {
                    return null;
                }
            }

            // Save changes
            await _userService.UpdateUserAsync(user);

            // Return updated user
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
