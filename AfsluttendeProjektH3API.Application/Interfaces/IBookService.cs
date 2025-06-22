using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application.Interfaces
{
    public interface IBookService
    {
        Task AddAsync(Book book);
        Task DeleteAsync(int id);
        Task<IEnumerable<Book>> GetAllAsync(string? title, string? genre, string? authorName);
        Task<IEnumerable<Book>> GetAllBooksByAuthorNameAsync(string? authorName);
        Task<Book?> GetAsync(int id);
        Task UpdateAsync(Book book);
    }
}