using Project_IV.Entities;

namespace Project_IV.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public Gender? Gender { get; set; }
        public State State { get; set; } = State.Offline;
        public int Age { get; set; }
        public ICollection<ImageDto> Images { get; set; } = new List<ImageDto>();
    }
}
