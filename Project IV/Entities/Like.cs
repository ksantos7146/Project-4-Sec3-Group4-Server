namespace Project_IV.Entities
{
    public class Like
    {
        public int LikeId { get; set; }
        public string LikerId { get; set; } = string.Empty;
        public string LikedId { get; set; } = string.Empty;
        public DateTime LikedAt { get; set; }

        // Navigation properties
        public User Liker { get; set; } = null!;
        public User Liked { get; set; } = null!;
    }
}
