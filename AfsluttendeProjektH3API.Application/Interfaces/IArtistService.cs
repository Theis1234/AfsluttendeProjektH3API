using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application.Interfaces
{
    public interface IArtistService
    {
        Task AddAsync(Artist artist);
        Task DeleteAsync(int id);
        Task<IEnumerable<Artist>> GetAllArtistsByFirstNameAsync(string? firstName);
        Task<IEnumerable<Artist>> GetAllArtistsByLastNameAsync(string? lastName);
        Task<IEnumerable<Artist>> GetAllArtistsByNationalityAsync(string? nationality);
        Task<IEnumerable<Artist>> GetAllAsync(string? firstName, string? lastName, string? nationality);
        Task<Artist?> GetAsync(int id);
        Task UpdateAsync(Artist artist);
    }
}