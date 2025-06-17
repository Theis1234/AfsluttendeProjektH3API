using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application.Interfaces
{
	public interface ICoverRepository
	{
		Task AddAsync(Cover cover);
		Task DeleteAsync(int id);
		Task<IEnumerable<Cover>> GetAllAsync();
        Task<IEnumerable<Cover>> GetFilteredAsync(string? title, bool? digitalOnly);
        Task<Cover?> GetByIdAsync(int id);
		Task UpdateAsync(Cover cover);
		bool CoverExists(int id);
	}
}
