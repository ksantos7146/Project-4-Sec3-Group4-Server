namespace Project_IV.Entities
{
    public class Image
    {
        private int imageId;
        private string imageData = string.Empty;
        private DateTime uploadedAt;
        private int userId;
        private User? user;

        public int ImageId { get => imageId; set => imageId = value; }
        public string ImageData { get => imageData; set => imageData = value; }
        public DateTime UploadedAt { get => uploadedAt; set => uploadedAt = value; }
        public int UserId { get => userId; set => userId = value; }
        public User? User { get => user; set => user = value; }
    }
}
