using AfsluttendeProjektH3API.Application.Interfaces;
using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application;

public class PublisherService : IPublisherService
{
    private readonly IGenericRepository<Publisher> _publisherRepository;

    public PublisherService(IGenericRepository<Publisher> publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }

    public async Task<IEnumerable<Publisher>> GetAllPublishersAsync() => await _publisherRepository.GetAllAsync();
    public Task<Publisher?> GetAsync(int id) => _publisherRepository.GetByIdAsync(id);
    public Task AddAsync(Publisher publisher) => _publisherRepository.AddAsync(publisher);
    public Task UpdateAsync(Publisher publisher) => _publisherRepository.UpdateAsync(publisher);
    public Task DeleteAsync(int id) => _publisherRepository.DeleteAsync(id);
}
