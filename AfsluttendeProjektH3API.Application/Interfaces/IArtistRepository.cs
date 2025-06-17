using AfsluttendeProjektH3API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AfsluttendeProjektH3API.Application.Interfaces
{
	public interface IArtistRepository
	{
		Task AddAsync(Artist artist);
		Task DeleteAsync(int id);
		Task<IEnumerable<Artist>> GetAllAsync();
        Task<IEnumerable<Artist>> GetFilteredAsync(string? firstName, string? lastName, string? nationality);
		Task<Artist?> GetByIdAsync(int id);
		Task UpdateAsync(Artist artist);
		bool ArtistExists(int id);
    }
}