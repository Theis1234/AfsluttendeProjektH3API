using AfsluttendeProjektH3API.Application.Interfaces;
using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application;

public class GenreService
{
    private readonly IGenericRepository<Genre> _genreRepository;

    public GenreService(IGenericRepository<Genre> genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<IEnumerable<Genre>> GetAllGenresAsync() => await _genreRepository.GetAllAsync();
    public Task<Genre?> GetAsync(int id) => _genreRepository.GetByIdAsync(id);
    public Task AddAsync(Genre genre) => _genreRepository.AddAsync(genre);
    public Task UpdateAsync(Genre genre) => _genreRepository.UpdateAsync(genre);
    public Task DeleteAsync(int id) => _genreRepository.DeleteAsync(id);
}
