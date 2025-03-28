namespace Project_IV.Entities
{
    public class Image
    {
        public int ImageId { get; set; }
        public string ImageData { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; }
        public string UserId { get; set; } = string.Empty;

        // Navigation property
        public User User { get; set; } = null!;
    }
}
