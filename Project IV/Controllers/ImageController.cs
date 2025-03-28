using Microsoft.AspNetCore.Mvc;
using Project_IV.Dtos;
using Project_IV.Endpoints;

namespace Project_IV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ImageEndpoint _imageEndpoint;

        public ImageController(ImageEndpoint imageEndpoint)
        {
            _imageEndpoint = imageEndpoint;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageDto>>> GetAllImages()
        {
            var images = await _imageEndpoint.GetAllImages();
            return Ok(images);
        }

        // Get a single image by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ImageDto>> GetImage(int id)
        {
            var image = await _imageEndpoint.GetImageById(id);
            if (image == null)
            {
                return NotFound();
            }
            return Ok(image);
        }

        // Create a new image
        [HttpPost]
        public async Task<ActionResult<ImageDto>> PostImage(ImageDto imageDto)
        {
            var createdImage = await _imageEndpoint.CreateImage(imageDto);
            return CreatedAtAction(nameof(GetImage), new { id = createdImage.ImageId }, createdImage);
        }

        // Update an existing image
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImage(int id, ImageDto imageDto)
        {
            var updatedImage = await _imageEndpoint.UpdateImage(id, imageDto);
            if (updatedImage == null)
            {
                return NotFound();
            }
            return Ok(updatedImage);
        }

        // Delete an image
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var result = await _imageEndpoint.DeleteImage(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Get all images for a specific user
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ImageDto>>> GetImagesByUserId(string userId)
        {
            var images = await _imageEndpoint.GetImagesByUserId(userId);
            return Ok(images);
        }

        // Create multiple images for a specific user
        [HttpPost("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ImageDto>>> PostImagesForUser(string userId, [FromBody] IEnumerable<ImageDto> images)
        {
            var createdImages = await _imageEndpoint.CreateImagesForUser(userId, images);
            return Ok(createdImages);
        }
    }
}
