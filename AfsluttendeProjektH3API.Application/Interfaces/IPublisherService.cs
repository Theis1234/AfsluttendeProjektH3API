using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application.Interfaces
{
    public interface IPublisherService
    {
        Task AddAsync(Publisher publisher);
        Task DeleteAsync(int id);
        Task<IEnumerable<Publisher>> GetAllPublishersAsync();
        Task<Publisher?> GetAsync(int id);
        Task UpdateAsync(Publisher publisher);
    }
}