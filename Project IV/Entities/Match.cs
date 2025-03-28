namespace Project_IV.Entities
{
    public class Match
    {
        public int MatchId { get; set; }
        public string User1Id { get; set; } = string.Empty;
        public string User2Id { get; set; } = string.Empty;
        public DateTime MatchedAt { get; set; }

        // Navigation properties
        public User User1 { get; set; } = null!;
        public User User2 { get; set; } = null!;
    }
}
