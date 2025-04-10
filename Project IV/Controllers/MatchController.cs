using Microsoft.AspNetCore.Mvc;
using Project_IV.Dtos;
using Project_IV.Endpoints;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Project_IV.Controllers
{
    [Route("api/matches")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly MatchEndpoint _matchEndpoint;

        // Injecting the MatchEndpoint through constructor
        public MatchController(MatchEndpoint matchEndpoint)
        {
            _matchEndpoint = matchEndpoint;
        }

        // Get a match by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<MatchDto>> GetMatch(int id)
        {
            var match = await _matchEndpoint.GetMatchById(id);
            if (match == null) return NotFound();
            return Ok(match);
        }

        // Create a new match
        [HttpPost]
        public async Task<ActionResult<MatchDto>> PostMatch([FromBody] MatchDto matchDto)
        {
            var createdMatch = await _matchEndpoint.CreateMatch(matchDto);
            return CreatedAtAction(nameof(GetMatch), new { id = createdMatch.MatchId }, createdMatch);
        }

        // Delete a match
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            var success = await _matchEndpoint.DeleteMatch(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<MatchDto>>> GetMatchesForUser(string userId)
        {
            var matches = await _matchEndpoint.GetMatchesForUser(userId);
            return Ok(matches);
        }
    }
}
