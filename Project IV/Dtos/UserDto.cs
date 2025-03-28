using Project_IV.Entities;

namespace Project_IV.Dtos
{
    public class UserDto
    {
        public int? UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public int? GenderId { get; set; }
        public int StateId { get; set; }
        public int Age { get; set; }
        public ICollection<ImageDto> Images { get; set; } = new List<ImageDto>();
    }
}
