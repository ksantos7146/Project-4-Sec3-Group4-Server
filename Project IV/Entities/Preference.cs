namespace Project_IV.Entities
{
    public class Preference
    {
        public int PreferenceId { get; set; }
        public int UserId { get; set; }
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = 100;
        public Gender? Gender { get; set; }
        public User? User { get; set; }
    }
}
