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
		bool AuthorExists(int id);
        Task<IEnumerable<Author>> GetFilteredAsync(string? firstName, string? lastName, string? nationality);
        Task<IEnumerable<Author>> GetAuthorsByNationality(string? nationality);
        Task<IEnumerable<Author>> GetByAuthorsFirstName(string? authorFirstName);
        Task<IEnumerable<Author>> GetByAuthorsLastName(string? authorLastName);
    }
}
