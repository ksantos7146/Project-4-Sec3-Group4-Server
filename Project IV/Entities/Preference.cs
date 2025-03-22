namespace Project_IV.Entities
{
    public class Preference
    {
        public int PreferenceId { get; set; }
        public int UserId { get; set; }
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = 100;
        public int? GenderId { get; set; }

        // Navigation properties
        public User User { get; set; } = null!;
        public Gender? Gender { get; set; }
    }
}
