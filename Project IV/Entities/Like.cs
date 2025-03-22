namespace Project_IV.Entities
{
    public class Like
    {
        public int LikeId { get; set; }
        public int LikerId { get; set; }
        public int LikedId { get; set; }
        public DateTime LikedAt { get; set; }

        // Navigation properties
        public User Liker { get; set; } = null!;
        public User Liked { get; set; } = null!;
    }
}
