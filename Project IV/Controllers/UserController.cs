using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Project_IV.Dtos;
using Project_IV.Endpoints;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserEndpoint _userEndpoint;
    private readonly IAuthService _authService;

    public UserController(UserEndpoint userEndpoint, IAuthService authService)
    {
        _userEndpoint = userEndpoint;
        _authService = authService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        var users = await _userEndpoint.GetAllUsers();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(string id)
    {
        var user = await _userEndpoint.GetUserById(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpGet("logged")]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetLoggedUser()
    {
        var user = await _userEndpoint.GetLoggedUser();
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
    [Authorize]
    public async Task<IActionResult> PutUser(string id, UserDto userDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Verify that the user is updating their own profile
        var currentUserId = await _authService.GetCurrentUserIdAsync();
        if (currentUserId != id)
        {
            return Forbid();
        }

        var updatedUser = await _userEndpoint.UpdateUser(userDto);
        if (updatedUser == null) return NotFound();
        return Ok(updatedUser);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var success = await _userEndpoint.DeleteUser(id);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpGet("{userId}/images")]
    public async Task<ActionResult<IEnumerable<ImageDto>>> GetImagesByUserId(string userId)
    {
        var images = await _userEndpoint.GetImagesByUserId(userId);
        return Ok(images);
    }

    [HttpPost("{userId}/images")]
    public async Task<ActionResult<IEnumerable<ImageDto>>> PostImagesByUserId(string userId, [FromBody] IEnumerable<ImageDto> images)
    {
        var createdImages = await _userEndpoint.CreateImagesForUser(userId, images);
        return Ok(createdImages);
    }
}
