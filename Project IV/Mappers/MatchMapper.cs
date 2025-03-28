using Project_IV.Dtos;
using Project_IV.Entities;

namespace Project_IV.Mappers
{
    public static class MatchMapper
    {
        public static MatchDto ToDto(this Match match)
        {
            return new MatchDto
            {
                MatchId = match.MatchId,
                User1Id = match.User1Id,
                User2Id = match.User2Id,
                MatchedAt = match.MatchedAt
            };
        }
        public static Match ToEntity(this MatchDto matchDto)
        {
            return new Match
            {
                User1Id = matchDto.User1Id,
                User2Id = matchDto.User2Id,
                MatchedAt = matchDto.MatchedAt
            };
        }
    }
}
