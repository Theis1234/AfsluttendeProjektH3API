using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application.Interfaces
{
    public interface IEditionService
    {
        Task AddAsync(Edition edition);
        Task DeleteAsync(int id);
        Task<IEnumerable<Edition>> GetAllEditionsAsync();
        Task<Edition?> GetAsync(int id);
        Task UpdateAsync(Edition edition);
    }
}