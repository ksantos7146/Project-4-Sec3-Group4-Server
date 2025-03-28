namespace Project_IV.Dtos
{
    public class MatchDto
    {
        public int? MatchId { get; set; }
        public int User1Id { get; set; }
        public int User2Id { get; set; }
        public DateTime MatchedAt { get; set; }
    }
}
