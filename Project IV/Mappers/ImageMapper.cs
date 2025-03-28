using Project_IV.Dtos;
using Project_IV.Entities;

namespace Project_IV.Mappers
{
    public static class ImageMapper
    {
        public static ImageDto ToDto(this Image image)
        {
            return new ImageDto
            {
                ImageId = image.ImageId,
                ImageData = image.ImageData,
                UploadedAt = image.UploadedAt,
                UserId = image.UserId
            };
        }
        public static Image ToEntity(this ImageDto imageDto)
        {
            return new Image
            {
                ImageData = imageDto.ImageData,
                UploadedAt = imageDto.UploadedAt,
                UserId = imageDto.UserId
            };
        }


    }
}
