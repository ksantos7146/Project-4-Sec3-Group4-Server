namespace Project_IV.Entities
{
    public class Preference
    {
        private int preferenceId;
        private int userId;
        private int minAge = 18;
        private int maxAge = 100;
        private Gender? gender;
        private User? user;

        public int PreferenceId { get => preferenceId; set => preferenceId = value; }
        public int UserId { get => userId; set => userId = value; }
        public int MinAge { get => minAge; set => minAge = value; }
        public int MaxAge { get => maxAge; set => maxAge = value; }
        public Gender? Gender { get => gender; set => gender = value; }
        public User? User { get => user; set => user = value; }
    }
}
