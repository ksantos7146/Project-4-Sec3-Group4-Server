namespace Project_IV.Dtos
{
    public class LikeDto
    {
        public int? LikeId { get; set; }
        public string? LikerId { get; set; } = string.Empty;
        public string LikedId { get; set; } = string.Empty;
        public DateTime? LikedAt { get; set; }
        public bool likedBack { get; set; } = false;
    }
}
