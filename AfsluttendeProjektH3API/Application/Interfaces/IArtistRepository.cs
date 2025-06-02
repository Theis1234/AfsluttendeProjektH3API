using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application.Interfaces
{
	public interface IArtistRepository
	{
		Task AddAsync(Artist artist);
		Task DeleteAsync(int id);
		Task<IEnumerable<Artist>> GetAllAsync();
		Task<Artist?> GetByIdAsync(int id);
		Task UpdateAsync(Artist artist);
	}
}