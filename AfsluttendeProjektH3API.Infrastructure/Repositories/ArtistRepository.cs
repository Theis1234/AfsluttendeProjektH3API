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
			await _context.Artists.Include(a => a.ArtistCovers).FirstOrDefaultAsync(p => p.Id == id);

		public async Task<IEnumerable<Artist>> GetAllAsync() =>
			await _context.Artists.Include(a => a.ArtistCovers).ToListAsync();

        public async Task<IEnumerable<Artist>> GetFilteredAsync(string? firstName, string? lastName, string? nationality)
        {
            var query = _context.Artists.Include(a => a.ArtistCovers)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(firstName))
                query = query.Where(b => b.FirstName.ToLower().Contains(firstName.ToLower()));

            if (!string.IsNullOrWhiteSpace(lastName))
                query = query.Where(b => b.LastName.ToLower().Contains(lastName.ToLower()));

            if (!string.IsNullOrWhiteSpace(nationality))
                query = query.Where(b => b.Nationality.CountryName.ToLower().Contains(nationality.ToLower()));

            return await query.ToListAsync();
        }
        public async Task<IEnumerable<Artist>> GetByArtistLastName(string? artistLastName)
        {
            var query = _context.Artists.Include(a => a.ArtistCovers)               
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(artistLastName))
                query = query.Where(b => b.LastName.ToLower() == artistLastName.ToLower());

            return await query.ToListAsync();
        }
        public async Task<IEnumerable<Artist>> GetByArtistFirstName(string? artistFirstName)
        {
            var query = _context.Artists.Include(a => a.ArtistCovers)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(artistFirstName))
                query = query.Where(b => b.FirstName.ToLower() == artistFirstName.ToLower());

            return await query.ToListAsync();
        }
        public async Task<IEnumerable<Artist>> GetArtistsByNationality(string? nationality)
        {
            var query = _context.Artists.Include(a => a.ArtistCovers)
                .Include(a => a.Nationality) 
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(nationality))
            {
                query = query.Where(a => a.Nationality != null &&
                                         a.Nationality.CountryName.ToLower() == nationality.ToLower());
            }

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
                var orphanCoverIds = artist.ArtistCovers
                .Select(ac => ac.CoverId)
                .Where(coverId =>
                    _context.ArtistCovers.Count(ac => ac.CoverId == coverId) == 1)
                .ToList();

                _context.Artists.Remove(artist);
				await _context.SaveChangesAsync();

                var orphanCovers = await _context.Covers
               .Where(c => orphanCoverIds.Contains(c.Id))
               .ToListAsync();

                _context.Covers.RemoveRange(orphanCovers);
                await _context.SaveChangesAsync();
            }
		}
		public bool ArtistExists(int id)
		{
			return _context.Artists.Any(e => e.Id == id);
		}
	}
}
