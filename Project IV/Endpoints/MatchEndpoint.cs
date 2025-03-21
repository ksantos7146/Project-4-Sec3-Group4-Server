using Project_IV.Dtos;
using Project_IV.Entities;
using Project_IV.Mappers;
using Project_IV.Service;

namespace Project_IV.Endpoints
{
    public class MatchEndpoint
    {
        private readonly IMatchService _matchService;

        public MatchEndpoint(IMatchService matchService)
        {
            _matchService = matchService;
        }

        // Get a match by ID
        public async Task<MatchDto> GetMatchById(int id)
        {
            var match = await _matchService.GetMatchByIdAsync(id);
            return match?.ToDto();
        }

        // Create a new match
        public async Task<MatchDto> CreateMatch(MatchDto matchDto)
        {
            var match = matchDto.ToEntity(); // Map DTO to entity
            await _matchService.AddMatchAsync(match);
            return match.ToDto(); // Return the created match as DTO
        }

        // Delete a match
        public async Task<bool> DeleteMatch(int id)
        {
            var match = await _matchService.GetMatchByIdAsync(id);
            if (match == null)
                return false;

            await _matchService.RemoveMatchAsync(id);
            return true;
        }
    }
}
