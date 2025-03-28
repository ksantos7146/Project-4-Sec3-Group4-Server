using Microsoft.AspNetCore.Mvc;
using Project_IV.Dtos;
using Project_IV.Endpoints;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project_IV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreferenceController : ControllerBase
    {
        private readonly PreferenceEndpoint _preferenceEndpoint;

        // Injecting the PreferenceEndpoint through constructor
        public PreferenceController(PreferenceEndpoint preferenceEndpoint)
        {
            _preferenceEndpoint = preferenceEndpoint;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PreferenceDto>>> GetAllPreferences()
        {
            var preferences = await _preferenceEndpoint.GetAllPreferences();
            return Ok(preferences);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<PreferenceDto>> GetPreferenceByUserId(string userId)
        {
            var preference = await _preferenceEndpoint.GetPreferenceByUserId(userId);
            if (preference == null)
            {
                return NotFound();
            }
            return Ok(preference);
        }

        // Create a new preference
        [HttpPost]
        public async Task<ActionResult<PreferenceDto>> CreatePreference(PreferenceDto preferenceDto)
        {
            var createdPreference = await _preferenceEndpoint.CreatePreference(preferenceDto);
            return CreatedAtAction(nameof(GetPreferenceByUserId), new { userId = createdPreference.UserId }, createdPreference);
        }

        // Update a preference
        [HttpPut("user/{userId}")]
        public async Task<IActionResult> UpdatePreference(string userId, PreferenceDto preferenceDto)
        {
            var updatedPreference = await _preferenceEndpoint.UpdatePreference(userId, preferenceDto);
            if (updatedPreference == null)
            {
                return NotFound();
            }
            return Ok(updatedPreference);
        }

        // Delete a preference
        [HttpDelete("user/{userId}")]
        public async Task<IActionResult> DeletePreference(string userId)
        {
            var result = await _preferenceEndpoint.DeletePreference(userId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
