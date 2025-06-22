using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application.Interfaces
{
    public interface INationalityService
    {
        Task AddAsync(Nationality nationality);
        Task DeleteAsync(int id);
        Task<IEnumerable<Nationality>> GetAllNationalitiesAsync();
        Task<Nationality?> GetAsync(int id);
        Task UpdateAsync(Nationality nationality);
    }
}