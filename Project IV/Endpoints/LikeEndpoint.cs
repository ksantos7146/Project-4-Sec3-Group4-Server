using Project_IV.Dtos;
using Project_IV.Entities;
using Project_IV.Service;
using Project_IV.Mappers;

namespace Project_IV.Endpoints
{
    public class LikeEndpoint
    {
        private readonly ILikeService _likeService;

        public LikeEndpoint(ILikeService likeService)
        {
            _likeService = likeService;
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
            var like = likeDto.ToEntity(); // Map DTO to entity
            await _likeService.AddLikeAsync(like);
            return like.ToDto(); // Return the created like as DTO
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
