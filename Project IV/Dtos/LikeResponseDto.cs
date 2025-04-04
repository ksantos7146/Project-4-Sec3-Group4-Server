using Project_IV.Entities;

namespace Project_IV.Dtos
{
    public class LikeResponseDto
    {
        public LikeDto Like { get; set; }
        public UserDto NextUser { get; set; }
    }
} 