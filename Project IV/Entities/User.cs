using static System.Net.Mime.MediaTypeNames;

namespace Project_IV.Entities
{
    public class User
    {
        private int userId;
        private string username = string.Empty;
        private string? bio;
        private Gender? gender;
        private State state = State.Offline;
        private int age;
        private ICollection<Image> images = new List<Image>();

        public int UserId { get => userId; set => userId = value; }
        public string Username { get => username; set => username = value; }
        public string? Bio { get => bio; set => bio = value; }
        public Gender? Gender { get => gender; set => gender = value; }
        public State State { get => state; set => state = value; }
        public int Age { get => age; set => age = value; }
        public ICollection<Image> Images { get => images; set => images = value; }
    }

}
