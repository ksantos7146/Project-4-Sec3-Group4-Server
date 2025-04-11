using Project_IV.Dtos;
using Project_IV.Entities;
using Project_IV.Mappers;
using Project_IV.Service;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Project_IV.Endpoints
{
    public class UserEndpoint
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;
        private readonly IPreferenceService _preferenceService;

        public UserEndpoint(
            IUserService userService, 
            IAuthService authService, 
            UserManager<User> userManager,
            IPreferenceService preferenceService)
        {
            _userService = userService;
            _authService = authService;
            _userManager = userManager;
            _preferenceService = preferenceService;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var currentUserId = await _authService.GetCurrentUserIdAsync();
            if (currentUserId == null)
            {
                return Enumerable.Empty<UserDto>();
            }

            // Get current user's preferences
            var preferences = await _preferenceService.GetPreferenceByUserIdAsync(currentUserId);
            
            // Get all users except current user
            var users = await _userService.GetAllUsersAsync();
            users = users.Where(u => u.Id != currentUserId).ToList();

            // Apply preference filters
            if (preferences != null)
            {
                if (preferences.MinAge.HasValue)
                {
                    users = users.Where(u => u.Age >= preferences.MinAge.Value).ToList();
                }
                
                if (preferences.MaxAge.HasValue)
                {
                    users = users.Where(u => u.Age <= preferences.MaxAge.Value).ToList();
                }
                
                if (preferences.GenderId.HasValue)
                {
                    users = users.Where(u => u.GenderId == preferences.GenderId.Value).ToList();
                }
            }

            // Get users that current user has already liked
            var likedUserIds = await _userService.GetLikedUserIdsAsync(currentUserId);
            
            // Filter out users that have already been liked
            users = users.Where(u => !likedUserIds.Contains(u.Id)).ToList();

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

            user.UserName = userDto.Username;
            user.Email = userDto.Email;
            user.Bio = userDto.Bio;
            user.GenderId = userDto.GenderId;
            user.Age = userDto.Age;

            await _userService.UpdateUserAsync(user);
            return user.ToDto();
        }

        public async Task UpdateUserState(string userId, int stateId)
        {
            await _userService.UpdateUserStateAsync(userId, stateId);
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
