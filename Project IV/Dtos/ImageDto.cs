namespace Project_IV.Dtos
{
    public class ImageDto
    {
        public int ImageId { get; set; }
        public string ImageData { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; }
        public int UserId { get; set; }
    }

}
