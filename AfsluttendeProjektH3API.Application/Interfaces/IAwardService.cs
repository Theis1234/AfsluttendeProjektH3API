using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application.Interfaces
{
    public interface IAwardService
    {
        Task AddAsync(Award award);
        Task DeleteAsync(int id);
        Task<IEnumerable<Award>> GetAllAwardsAsync();
        Task<Award?> GetAsync(int id);
        Task<IEnumerable<Award>> GetAwardsByArtistIdAsync(int artistId);
        Task UpdateAsync(Award award);
    }
}