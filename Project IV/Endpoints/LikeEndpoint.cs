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

        public LikeEndpoint(ILikeService likeService, IAuthService authService, IMatchService matchService)
        {
            _likeService = likeService;
            _authService = authService;
            _matchService = matchService;
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
        public async Task<LikeDto> CreateLike(LikeDto likeDto)
        {
            var likerId = await _authService.GetCurrentUserIdAsync();
            var existingLike = (await _likeService.GetLikesByLikedIdAsync(likeDto.LikedId))
                               .FirstOrDefault(l => l.LikerId == likeDto.LikedId);

            var like = likeDto.ToEntity();
            like.LikedAt = DateTime.UtcNow;
            like.LikerId = likerId;
            await _likeService.AddLikeAsync(like);
            if (existingLike != null)
            {
                var match = new Match
                {
                    MatchedAt = DateTime.UtcNow,
                    User1Id = existingLike.LikerId,
                    User2Id = like.LikedId
                };
                await _matchService.AddMatchAsync(match);
                return new LikeDto
                {
                    LikedAt = like.LikedAt,
                    LikerId = like.LikerId,
                    LikedId = existingLike.LikerId,
                    likedBack = true
                };
            }
            return like.ToDto(); // Return the like as a DTO
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
