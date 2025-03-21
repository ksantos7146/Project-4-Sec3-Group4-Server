using Microsoft.AspNetCore.Mvc;
using Project_IV.Dtos;
using Project_IV.Endpoints;
using System.Threading.Tasks;

namespace Project_IV.Controllers
{
    [Route("api/preferences")]
    [ApiController]
    public class PreferenceController : ControllerBase
    {
        private readonly PreferenceEndpoint _preferenceEndpoint;

        // Injecting the PreferenceEndpoint through constructor
        public PreferenceController(PreferenceEndpoint preferenceEndpoint)
        {
            _preferenceEndpoint = preferenceEndpoint;
        }

        // Get a preference by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PreferenceDto>> GetPreference(int id)
        {
            var preference = await _preferenceEndpoint.GetPreferenceById(id);
            if (preference == null) return NotFound();
            return Ok(preference);
        }

        // Create a new preference
        [HttpPost]
        public async Task<ActionResult<PreferenceDto>> PostPreference([FromBody] PreferenceDto preferenceDto)
        {
            var createdPreference = await _preferenceEndpoint.CreatePreference(preferenceDto);
            return CreatedAtAction(nameof(GetPreference), new { id = createdPreference.PreferenceId }, createdPreference);
        }

        // Update a preference
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPreference(int id, [FromBody] PreferenceDto preferenceDto)
        {
            if (id != preferenceDto.PreferenceId) return BadRequest();
            var updatedPreference = await _preferenceEndpoint.UpdatePreference(id, preferenceDto);
            if (updatedPreference == null) return NotFound();
            return Ok(updatedPreference);
        }

        // Delete a preference
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePreference(int id)
        {
            var success = await _preferenceEndpoint.DeletePreference(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
