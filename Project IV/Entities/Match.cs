namespace Project_IV.Entities
{
    public class Match
    {
        private int matchId;
        private int user1Id;
        private int user2Id;
        private DateTime matchedAt;

        public int MatchId { get => matchId; set => matchId = value; }
        public int User1Id { get => user1Id; set => user1Id = value; }
        public int User2Id { get => user2Id; set => user2Id = value; }
        public DateTime MatchedAt { get => matchedAt; set => matchedAt = value; }
    }
}
