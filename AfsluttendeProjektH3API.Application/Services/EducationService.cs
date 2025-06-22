using AfsluttendeProjektH3API.Application.Interfaces;
using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application;

public class EducationService : IEducationService
{
    private readonly IGenericRepository<Education> _educationRepository;

    public EducationService(IGenericRepository<Education> educationRepository)
    {
        _educationRepository = educationRepository;
    }

    public async Task<IEnumerable<Education>> GetAllEducationsAsync() => await _educationRepository.GetAllAsync();
    public Task<Education?> GetAsync(int id) => _educationRepository.GetByIdAsync(id);
    public Task AddAsync(Education education) => _educationRepository.AddAsync(education);
    public Task UpdateAsync(Education education) => _educationRepository.UpdateAsync(education);
    public Task DeleteAsync(int id) => _educationRepository.DeleteAsync(id);
}
