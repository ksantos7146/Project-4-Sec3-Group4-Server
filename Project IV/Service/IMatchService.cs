using Project_IV.Entities;
namespace Project_IV.Service
{
    public interface IMatchService
    {
        Task<Match> GetMatchByIdAsync(int id);
        Task<IEnumerable<Match>> GetAllMatchesAsync();
        Task AddMatchAsync(Match match);
        Task RemoveMatchAsync(int id);
    }
}
