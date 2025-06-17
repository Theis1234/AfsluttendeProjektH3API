using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application.Interfaces
{
	public interface IBookRepository
	{
		Task AddAsync(Book book);
		Task DeleteAsync(int id);
		Task<IEnumerable<Book>> GetAllAsync();
        Task<IEnumerable<Book>> GetByAuthorLastName(string authorLastName);
        Task<IEnumerable<Book>> GetFilteredAsync(string? title, string? genre, string? authorName);
        Task<Book?> GetByIdAsync(int id);
        Task<Book?> GetByTitleAsync(string title);
        Task UpdateAsync(Book book);
		bool BookExists(int id);
	}
}
