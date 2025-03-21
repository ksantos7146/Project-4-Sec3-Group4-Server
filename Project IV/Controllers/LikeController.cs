using Microsoft.AspNetCore.Mvc;
using Project_IV.Dtos;
using Project_IV.Endpoints;

[Route("api/likes")]
[ApiController]
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
        if (like == null) return NotFound();
        return Ok(like);
    }

    // Create a new like
    [HttpPost]
    public async Task<ActionResult<LikeDto>> PostLike([FromBody] LikeDto likeDto)
    {
        var createdLike = await _likeEndpoint.CreateLike(likeDto);
        return CreatedAtAction(nameof(GetLike), new { id = createdLike.LikeId }, createdLike);
    }

    // Delete a like
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLike(int id)
    {
        var success = await _likeEndpoint.DeleteLike(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
