using Microsoft.AspNetCore.Mvc;
using Project_IV.Dtos;
using Project_IV.Service;

namespace Project_IV.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
        {
            var response = await _authService.RegisterAsync(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
        {
            var response = await _authService.LoginAsync(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("current-user")]
        public async Task<ActionResult<string>> GetCurrentUserId()
        {
            var userId = await _authService.GetCurrentUserIdAsync();
            if (userId == null)
            {
                return Unauthorized();
            }
            return Ok(userId);
        }
    }
} 