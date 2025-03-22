using Microsoft.AspNetCore.Mvc;
using Project_IV.Dtos;
using Project_IV.Endpoints;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserEndpoint _userEndpoint;

    public UserController(UserEndpoint userEndpoint)
    {
        _userEndpoint = userEndpoint;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _userEndpoint.GetUserById(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> PostUser(UserDto userDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var createdUser = await _userEndpoint.CreateUser(userDto);
        return CreatedAtAction(nameof(GetUser), new { id = createdUser.UserId }, createdUser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, UserDto userDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (id != userDto.UserId) return BadRequest();
        var updatedUser = await _userEndpoint.UpdateUser(id, userDto);
        if (updatedUser == null) return NotFound();
        return Ok(updatedUser);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var success = await _userEndpoint.DeleteUser(id);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpGet("{userId}/images")]
    public async Task<ActionResult<IEnumerable<ImageDto>>> GetImagesByUserId(int userId)
    {
        var images = await _userEndpoint.GetImagesByUserId(userId);
        return Ok(images);
    }

    [HttpPost("{userId}/images")]
    public async Task<ActionResult<IEnumerable<ImageDto>>> PostImagesByUserId(int userId, [FromBody] IEnumerable<ImageDto> images)
    {
        var createdImages = await _userEndpoint.CreateImagesByUserId(userId, images);
        return CreatedAtAction(nameof(GetImagesByUserId), new { userId = userId }, createdImages);
    }
}
