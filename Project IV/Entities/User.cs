namespace Project_IV.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public int? GenderId { get; set; }
        public int StateId { get; set; }
        public int Age { get; set; }
        
        // Navigation properties
        public Gender? Gender { get; set; }
        public State? State { get; set; }
        public ICollection<Image> Images { get; set; } = new List<Image>();
        public ICollection<Preference> Preferences { get; set; } = new List<Preference>();
        public ICollection<Like> LikesGiven { get; set; } = new List<Like>();
        public ICollection<Like> LikesReceived { get; set; } = new List<Like>();
        public ICollection<Match> MatchesAsUser1 { get; set; } = new List<Match>();
        public ICollection<Match> MatchesAsUser2 { get; set; } = new List<Match>();
    }
}
