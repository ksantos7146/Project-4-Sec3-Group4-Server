using Project_IV.Entities;

namespace Project_IV.Dtos
{
    public class PreferenceDto
    {
        public int? PreferenceId { get; set; }
        public int UserId { get; set; }
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = 100;
        public int? GenderId { get; set; }
    }

}
