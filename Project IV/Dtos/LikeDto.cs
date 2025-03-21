namespace Project_IV.Dtos
{
    public class LikeDto
    {
        public int LikeId { get; set; }
        public int LikerId { get; set; }
        public int LikedId { get; set; }
        public DateTime LikedAt { get; set; }
    }
}
