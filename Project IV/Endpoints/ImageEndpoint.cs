using Project_IV.Dtos;
using Project_IV.Entities;
using Project_IV.Service.Impl;
using Project_IV.Mappers;
using Project_IV.Service;

namespace Project_IV.Endpoints
{
    public class ImageEndpoint
    {
        private readonly IImageService _imageService;

        public ImageEndpoint(IImageService imageService)
        {
            _imageService = imageService;
        }

        // Get a single image by ID
        public async Task<ImageDto> GetImageById(int id)
        {
            var image = await _imageService.GetImageByIdAsync(id);
            return image?.ToDto(); // Map to DTO if image is found
        }

        // Get all images
        public async Task<IEnumerable<ImageDto>> GetAllImages()
        {
            var images = await _imageService.GetAllImagesAsync();
            return images.Select(i => i.ToDto()); // Map all images to DTOs
        }

        // Create a new image
        public async Task<ImageDto> CreateImage(ImageDto imageDto)
        {
            var image = imageDto.ToEntity(); // Map DTO to entity
            await _imageService.AddImageAsync(image);
            return image.ToDto(); // Return the created image as DTO
        }

        // Update an existing image
        public async Task<ImageDto> UpdateImage(int id, ImageDto imageDto)
        {
            var existingImage = await _imageService.GetImageByIdAsync(id);
            if (existingImage == null)
                return null;

            var updatedImage = imageDto.ToEntity(); // Map DTO to entity
            updatedImage.ImageId = id; // Ensure correct ID is set

            await _imageService.UpdateImageAsync(updatedImage);
            return updatedImage.ToDto(); // Return the updated image as DTO
        }

        // Delete an image
        public async Task<bool> DeleteImage(int id)
        {
            var image = await _imageService.GetImageByIdAsync(id);
            if (image == null)
                return false;

            await _imageService.DeleteImageAsync(id);
            return true;
        }

        // Get all images for a specific user
        public async Task<IEnumerable<ImageDto>> GetImagesByUserId(string userId)
        {
            var images = await _imageService.GetImagesByUserIdAsync(userId);
            return images.Select(i => i.ToDto());
        }

        // Create multiple images for a specific user
        public async Task<IEnumerable<ImageDto>> CreateImagesForUser(string userId, IEnumerable<ImageDto> imageDtos)
        {
            var images = imageDtos.Select(dto => dto.ToEntity()).ToList(); // Map DTOs to entities
            foreach (var image in images)
            {
                image.UserId = userId; // Set the user ID for each image
                await _imageService.AddImageAsync(image);
            }
            return images.Select(i => i.ToDto());
        }
    }
}
