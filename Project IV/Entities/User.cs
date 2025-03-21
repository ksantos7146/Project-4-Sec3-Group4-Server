namespace Project_IV.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public Gender? Gender { get; set; }
        public State State { get; set; } = State.Offline;
        public int Age { get; set; }
        public ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
