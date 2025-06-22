using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application.Interfaces
{
    public interface IEducationService
    {
        Task AddAsync(Education education);
        Task DeleteAsync(int id);
        Task<IEnumerable<Education>> GetAllEducationsAsync();
        Task<Education?> GetAsync(int id);
        Task UpdateAsync(Education education);
    }
}