namespace Project_IV.Entities
{
    public class Preference
    {
        public int PreferenceId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public int? GenderId { get; set; }
        public int? StateId { get; set; }

        // Navigation properties
        public User User { get; set; } = null!;
        public Gender? Gender { get; set; }
        public State? State { get; set; }
    }
}
