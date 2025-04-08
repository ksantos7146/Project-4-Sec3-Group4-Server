using Microsoft.AspNetCore.Mvc;
using Project_IV.Dtos;
using Project_IV.Endpoints;
using Project_IV.Entities;
using Project_IV.Mappers;

namespace Project_IV.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LikeController : ControllerBase
    {
        private readonly LikeEndpoint _likeEndpoint;

        public LikeController(LikeEndpoint likeEndpoint)
        {
            _likeEndpoint = likeEndpoint;
        }

        // Get a single like by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<LikeDto>> GetLike(int id)
        {
            var like = await _likeEndpoint.GetLikeById(id);
            if (like == null)
                return NotFound();
            return Ok(like);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeDto>>> GetAllLikes()
        {
            var likes = await _likeEndpoint.GetAllLikes();
            return Ok(likes);
        }

        // Create a new like
        [HttpPost]
        public async Task<ActionResult<LikeResponseDto>> PostLike(LikeDto likeDto)
        {
            try
            {
                if (likeDto == null)
                {
                    return BadRequest("Like data is required");
                }

                if (string.IsNullOrEmpty(likeDto.LikedId))
                {
                    return BadRequest("Liked user ID is required");
                }

                var response = await _likeEndpoint.CreateLike(likeDto);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error in PostLike: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return StatusCode(500, new { message = "An error occurred while processing your like request. Please try again later." });
            }
        }

        // Delete a like
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLike(int id)
        {
            var result = await _likeEndpoint.DeleteLike(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
