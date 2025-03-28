using Project_IV.Entities;

namespace Project_IV.Dtos
{
    public class PreferenceDto
    {
        public int? PreferenceId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public int? GenderId { get; set; }
        public int? StateId { get; set; }
    }

}
