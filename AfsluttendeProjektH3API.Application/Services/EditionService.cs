using AfsluttendeProjektH3API.Application.Interfaces;
using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application;

public class EditionService : IEditionService
{
    private readonly IGenericRepository<Edition> _editionRepository;

    public EditionService(IGenericRepository<Edition> editionRepository)
    {
        _editionRepository = editionRepository;
    }

    public async Task<IEnumerable<Edition>> GetAllEditionsAsync() => await _editionRepository.GetAllAsync();
    public Task<Edition?> GetAsync(int id) => _editionRepository.GetByIdAsync(id);
    public Task AddAsync(Edition edition) => _editionRepository.AddAsync(edition);
    public Task UpdateAsync(Edition edition) => _editionRepository.UpdateAsync(edition);
    public Task DeleteAsync(int id) => _editionRepository.DeleteAsync(id);
}
