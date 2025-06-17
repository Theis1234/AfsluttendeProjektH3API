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

        public async Task<IEnumerable<Artist>> GetFilteredAsync(string? firstName, string? lastName, string? nationality)
        {
            var query = _context.Artists
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(firstName))
                query = query.Where(b => b.FirstName.Contains(firstName));

            if (!string.IsNullOrWhiteSpace(lastName))
                query = query.Where(b => b.LastName.Contains(lastName));

            if (!string.IsNullOrWhiteSpace(nationality))
                query = query.Where(b => b.Nationality.Contains(nationality));

            return await query.ToListAsync();
        }
        public async Task<IEnumerable<Artist>> GetByArtistLastName(string? artistLastName)
        {
            var query = _context.Artists               
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(artistLastName))
                query = query.Where(b => b.LastName == artistLastName);

            return await query.ToListAsync();
        }
        public async Task<IEnumerable<Artist>> GetByArtistFirstName(string? artistFirstName)
        {
            var query = _context.Artists
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(artistFirstName))
                query = query.Where(b => b.FirstName == artistFirstName);

            return await query.ToListAsync();
        }
        public async Task<IEnumerable<Artist>> GetArtistsByNationality(string? nationality)
        {
            var query = _context.Artists
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(nationality))
                query = query.Where(b => b.Nationality == nationality);

            return await query.ToListAsync();
        }

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
            var artist = _context.Artists
				.Include(a => a.ArtistCovers)
				.ThenInclude(ac => ac.Cover)
				.FirstOrDefault(a => a.Id == id);

            if (artist != null)
			{
                var orphanCovers = artist.ArtistCovers
					.Where(ac => _context.ArtistCovers.Count(linkedAc => linkedAc.CoverId == ac.CoverId) == 1)
					.Select(ac => ac.Cover)
					.ToList();

                _context.Covers.RemoveRange(orphanCovers);

                _context.Artists.Remove(artist);
				await _context.SaveChangesAsync();
			}
		}
		public bool ArtistExists(int id)
		{
			return _context.Artists.Any(e => e.Id == id);
		}
	}
}
