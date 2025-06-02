using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application.Interfaces
{
	public interface IAuthorRepository
	{
		Task AddAsync(Author author);
		Task DeleteAsync(int id);
		Task<IEnumerable<Author>> GetAllAsync();
		Task<Author?> GetByIdAsync(int id);
		Task UpdateAsync(Author author);
	}
}
