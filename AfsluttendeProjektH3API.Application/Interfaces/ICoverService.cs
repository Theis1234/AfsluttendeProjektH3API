using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application.Interfaces
{
    public interface ICoverService
    {
        Task AddAsync(Cover cover);
        Task DeleteAsync(int id);
        Task<IEnumerable<Cover>> GetAllAsync(string? title, bool? digitalOnly);
        Task<Cover?> GetAsync(int id);
        Task<Cover?> GetCoverByBookIdAsync(int id);
        Task<IEnumerable<Cover>> GetCoversByArtistAsync(int artistId);
        Task<IEnumerable<Cover>> GetDigitalOnlyCoversAsync(bool digitalOnly);
        Task UpdateAsync(Cover cover);
    }
}