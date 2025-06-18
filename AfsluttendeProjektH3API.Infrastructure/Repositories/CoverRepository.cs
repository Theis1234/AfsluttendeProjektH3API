using AfsluttendeProjektH3API.Application.Interfaces;
using AfsluttendeProjektH3API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AfsluttendeProjektH3API.Infrastructure.Repositories
{
	public class CoverRepository : ICoverRepository
	{
		private readonly AppDbContext _context;
		public CoverRepository(AppDbContext context) => _context = context;

		public async Task<Cover?> GetByIdAsync(int id) =>
			await _context.Covers.Include(c => c.Book).Include(c => c.ArtistCovers).ThenInclude(ac => ac.Artist).FirstOrDefaultAsync(p => p.Id == id);

		public async Task<IEnumerable<Cover>> GetAllAsync() =>
			await _context.Covers.Include(c => c.Book).ToListAsync();

        public async Task<IEnumerable<Cover>> GetFilteredAsync(string? title, bool? digitalOnly)
        {
            var query = _context.Covers
				.Include(c => c.Book)  
				.AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(c => c.Title.Contains(title));
            }

            if (digitalOnly.HasValue)
            {
                query = query.Where(c => c.DigitalOnly == digitalOnly.Value);
            }

            return await query.ToListAsync();
        }

        public async Task AddAsync(Cover cover)
		{
			_context.Add(cover);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Cover cover)
		{
			_context.Covers.Update(cover);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await _context.Covers.FindAsync(id);
			if (entity != null)
			{
				_context.Covers.Remove(entity);
				await _context.SaveChangesAsync();
			}
		}
		public bool CoverExists(int id)
		{
			return _context.Covers.Any(c => c.Id == id);
		}

        public async Task<Cover?> GetCoverByBookIdAsync(int bookId)
        {
            return await _context.Covers.FirstOrDefaultAsync(a => a.BookId == bookId);
        }

        public async Task<IEnumerable<Cover>> GetCoversByArtistIdAsync(int artistId)
        {
            return await _context.Covers.Include(c => c.ArtistCovers).ThenInclude(ac => ac.Artist).Where(c => c.ArtistCovers.Any(ac => ac.ArtistId == artistId)).ToListAsync();
        }

        public async Task<IEnumerable<Cover>> GetDigitalOnlyCoversAsync(bool digitalOnly)
        {
            return await _context.Covers.Where(a => a.DigitalOnly == digitalOnly).ToListAsync();
        }
    }
}
