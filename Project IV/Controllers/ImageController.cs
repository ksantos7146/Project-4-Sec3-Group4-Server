using Microsoft.AspNetCore.Mvc;
using Project_IV.Dtos;
using Project_IV.Endpoints;

namespace Project_IV.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ImageEndpoint imageEndpoint;

        public ImageController(ImageEndpoint imageEndpoint)
        {
            this.imageEndpoint = imageEndpoint;
        }

        // Get a single image by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ImageDto>> GetImage(int id)
        {
            var image = await imageEndpoint.GetImageById(id);
            if (image == null) return NotFound();
            return Ok(image);
        }

        // Create a new image
        [HttpPost]
        public async Task<ActionResult<ImageDto>> PostImage([FromBody] ImageDto imageDto)
        {
            var createdImage = await imageEndpoint.CreateImage(imageDto);
            return CreatedAtAction(nameof(GetImage), new { id = createdImage.ImageId }, createdImage);
        }

        // Update an existing image
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImage(int id, [FromBody] ImageDto imageDto)
        {
            if (id != imageDto.ImageId) return BadRequest();
            var updatedImage = await imageEndpoint.UpdateImage(id, imageDto);
            if (updatedImage == null) return NotFound();
            return Ok(updatedImage);
        }

        // Delete an image
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var success = await imageEndpoint.DeleteImage(id);
            if (!success) return NotFound();
            return NoContent();
        }

        // Get all images for a specific user
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ImageDto>>> GetImagesByUserId(int userId)
        {
            var images = await imageEndpoint.GetImagesByUserId(userId);
            return Ok(images);
        }

        // Create multiple images for a specific user
        [HttpPost("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ImageDto>>> PostImagesByUserId(int userId, [FromBody] IEnumerable<ImageDto> images)
        {
            var createdImages = await imageEndpoint.CreateImagesByUserId(userId, images);
            return CreatedAtAction(nameof(GetImagesByUserId), new { userId = userId }, createdImages);
        }
    }
}
