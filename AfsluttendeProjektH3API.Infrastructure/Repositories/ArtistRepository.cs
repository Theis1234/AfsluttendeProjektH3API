using AfsluttendeProjektH3API.Application.Interfaces;
using AfsluttendeProjektH3API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AfsluttendeProjektH3API.Infrastructure.Repositories
{
	public class ArtistRepository : IArtistRepository
	{
		private readonly AppDbContext _context;
		public ArtistRepository(AppDbContext context) => _context = context;

		public async Task<Artist?> GetByIdAsync(int id) =>
			await _context.Artists.FirstOrDefaultAsync(p => p.Id == id);

		public async Task<IEnumerable<Artist>> GetAllAsync() =>
			await _context.Artists.ToListAsync();

		public async Task AddAsync(Artist artist)
		{
			_context.Add(artist);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Artist artist)
		{
			_context.Artists.Update(artist);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await _context.Artists.FindAsync(id);
			if (entity != null)
			{
				_context.Artists.Remove(entity);
				await _context.SaveChangesAsync();
			}
		}
		public bool ArtistExists(int id)
		{
			return _context.Artists.Any(e => e.Id == id);
		}
	}
}
