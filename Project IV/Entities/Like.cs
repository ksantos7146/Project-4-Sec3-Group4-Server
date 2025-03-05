namespace Project_IV.Entities
{
    public class Like
    {
        private int likeId;
        private int likerId;
        private int likedId;
        private DateTime likedAt;

        public int LikeId { get => likeId; set => likeId = value; }
        public int LikerId { get => likerId; set => likerId = value; }
        public int LikedId { get => likedId; set => likedId = value; }
        public DateTime LikedAt { get => likedAt; set => likedAt = value; }
    }
}
