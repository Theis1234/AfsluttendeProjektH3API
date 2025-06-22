using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application.Interfaces
{
    public interface IGenreService
    {
        Task AddAsync(Genre genre);
        Task DeleteAsync(int id);
        Task<IEnumerable<Genre>> GetAllGenresAsync();
        Task<Genre?> GetAsync(int id);
        Task UpdateAsync(Genre genre);
    }
}