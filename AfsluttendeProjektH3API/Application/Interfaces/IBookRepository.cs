using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application.Interfaces
{
	public interface IBookRepository
	{
		Task AddAsync(Book book);
		Task DeleteAsync(int id);
		Task<IEnumerable<Book>> GetAllAsync();
		Task<Book?> GetByIdAsync(int id);
		Task UpdateAsync(Book book);
	}
}
