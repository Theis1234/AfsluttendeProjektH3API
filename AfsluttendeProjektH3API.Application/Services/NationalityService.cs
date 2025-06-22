using AfsluttendeProjektH3API.Application.Interfaces;
using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application;

public class NationalityService
{
    private readonly IGenericRepository<Nationality> _nationalityRepository;

    public NationalityService(IGenericRepository<Nationality> nationalityRepository)
    {
        _nationalityRepository = nationalityRepository;
    }

    public async Task<IEnumerable<Nationality>> GetAllNationalitiesAsync() => await _nationalityRepository.GetAllAsync();
    public Task<Nationality?> GetAsync(int id) => _nationalityRepository.GetByIdAsync(id);
    public Task AddAsync(Nationality nationality) => _nationalityRepository.AddAsync(nationality);
    public Task UpdateAsync(Nationality nationality) => _nationalityRepository.UpdateAsync(nationality);
    public Task DeleteAsync(int id) => _nationalityRepository.DeleteAsync(id);
}
