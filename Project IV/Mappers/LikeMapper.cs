using Project_IV.Dtos;
using Project_IV.Entities;

namespace Project_IV.Mappers
{
    public static class LikeMapper
    {
        public static LikeDto ToDto(this Like like)
        {
            return new LikeDto
            {
                LikeId = like.LikeId,
                LikerId = like.LikerId,
                LikedId = like.LikedId,
                LikedAt = like.LikedAt
            };
        }
        public static Like ToEntity(this LikeDto likeDto)
        {
            return new Like
            {
                LikeId = likeDto.LikeId,
                LikerId = likeDto.LikerId,
                LikedId = likeDto.LikedId,
                LikedAt = likeDto.LikedAt
            };
        }

    }
}
