namespace Project_IV.Entities
{
    public class Like
    {
        public int LikeId { get; set; }
        public int LikerId { get; set; }
        public int LikedId { get; set; }
        public DateTime LikedAt { get; set; }
    }
}
