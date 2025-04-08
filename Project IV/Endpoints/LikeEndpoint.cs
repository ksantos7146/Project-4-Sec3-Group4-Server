using Project_IV.Dtos;
using Project_IV.Entities;
using Project_IV.Service;
using Project_IV.Mappers;
using System.Collections.Generic;

namespace Project_IV.Endpoints
{
    public class LikeEndpoint
    {
        private readonly ILikeService _likeService;
        private readonly IAuthService _authService;
        private readonly IMatchService _matchService;
        private readonly IUserService _userService;

        public LikeEndpoint(ILikeService likeService, IAuthService authService, IMatchService matchService, IUserService userService)
        {
            _likeService = likeService;
            _authService = authService;
            _matchService = matchService;
            _userService = userService;
        }

        // Get a single like by ID
        public async Task<LikeDto> GetLikeById(int id)
        {
            var like = await _likeService.GetLikeByIdAsync(id);
            return like?.ToDto(); // Map to DTO if like is found
        }

        // Get all likes
        public async Task<IEnumerable<LikeDto>> GetAllLikes()
        {
            var likes = await _likeService.GetAllLikesAsync();
            return likes.Select(l => l.ToDto()); // Map all likes to DTOs
        }

        // Create a new like
        public async Task<LikeResponseDto> CreateLike(LikeDto likeDto)
        {
            try
            {
                var likerId = await _authService.GetCurrentUserIdAsync();
                if (string.IsNullOrEmpty(likerId))
                {
                    throw new UnauthorizedAccessException("User is not authenticated");
                }

                // Validate that both users exist
                var liker = await _userService.GetUserByIdAsync(likerId);
                var likedUser = await _userService.GetUserByIdAsync(likeDto.LikedId);
                
                if (liker == null)
                {
                    throw new InvalidOperationException($"Liker user with ID {likerId} not found");
                }
                if (likedUser == null)
                {
                    throw new InvalidOperationException($"Liked user with ID {likeDto.LikedId} not found");
                }
                
                // Check if the like already exists
                var existingLike = (await _likeService.GetLikesByLikerIdAsync(likerId))
                                   .FirstOrDefault(l => l.LikedId == likeDto.LikedId);
                
                if (existingLike != null)
                {
                    // Like already exists, return it
                    return new LikeResponseDto
                    {
                        Like = existingLike.ToDto(),
                        NextUser = null
                    };
                }

                // Check if the other user has already liked the current user
                var likedBack = (await _likeService.GetLikesByLikedIdAsync(likerId))
                                .Any(l => l.LikerId == likeDto.LikedId);

                var like = likeDto.ToEntity();
                like.LikedAt = DateTime.UtcNow;
                like.LikerId = likerId;
                await _likeService.AddLikeAsync(like);

                // Get a new user to show next
                var nextUser = await GetNextUserToShow(likerId);
                
                var response = new LikeResponseDto
                {
                    Like = like.ToDto(),
                    NextUser = nextUser?.ToDto()
                };

                if (likedBack)
                {
                    var match = new Match
                    {
                        MatchedAt = DateTime.UtcNow,
                        User1Id = likerId,
                        User2Id = likeDto.LikedId
                    };
                    await _matchService.AddMatchAsync(match);
                    response.Like.likedBack = true;
                }

                return response;
            }
            catch (Exception ex)
            {
                // Log the error details
                Console.WriteLine($"Error in CreateLike: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw; // Re-throw to be handled by the controller
            }
        }

        private async Task<User?> GetNextUserToShow(string currentUserId)
        {
            // Get all users except the current user and those already liked
            var allUsers = await _userService.GetAllUsersAsync();
            var likedUsers = (await _likeService.GetLikesByLikerIdAsync(currentUserId))
                .Select(l => l.LikedId)
                .ToList();

            var availableUsers = allUsers
                .Where(u => u.Id != currentUserId && !likedUsers.Contains(u.Id))
                .ToList();

            if (!availableUsers.Any())
                return null;

            // Return a random user from the available ones
            var random = new Random();
            return availableUsers[random.Next(availableUsers.Count)];
        }

        // Delete a like
        public async Task<bool> DeleteLike(int id)
        {
            var like = await _likeService.GetLikeByIdAsync(id);
            if (like == null)
                return false;

            await _likeService.RemoveLikeAsync(id);
            return true;
        }
    }
}
