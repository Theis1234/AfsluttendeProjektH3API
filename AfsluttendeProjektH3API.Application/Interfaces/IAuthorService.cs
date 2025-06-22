using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application.Interfaces
{
    public interface IAuthorService
    {
        Task AddAsync(Author author);
        Task DeleteAsync(int id);
        Task<IEnumerable<Author>> GetAllAsync(string? firstName, string? lastName, string? nationality);
        Task<IEnumerable<Author>> GetAllAuthorsByFirstNameAsync(string? firstName);
        Task<IEnumerable<Author>> GetAllAuthorsByLastNameAsync(string? lastName);
        Task<IEnumerable<Author>> GetAllAuthorsByNationalityAsync(string? nationality);
        Task<Author?> GetAsync(int id);
        Task UpdateAsync(Author author);
    }
}