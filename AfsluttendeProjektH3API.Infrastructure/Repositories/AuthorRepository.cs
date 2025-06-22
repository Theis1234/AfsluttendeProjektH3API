using AfsluttendeProjektH3API.Application.Interfaces;
using AfsluttendeProjektH3API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AfsluttendeProjektH3API.Infrastructure.Repositories
{
	public class AuthorRepository : IAuthorRepository
	{
		private readonly AppDbContext _context;
		public AuthorRepository(AppDbContext context) => _context = context;

		public async Task<Author?> GetByIdAsync(int id) =>
			await _context.Authors
            .Include(a => a.Nationality)
            .Include(a => a.Publisher)
            .Include(a => a.Education)
            .FirstOrDefaultAsync(p => p.Id == id);

		public async Task<IEnumerable<Author>> GetAllAsync() =>
			await _context.Authors
            .Include(a => a.Nationality)
            .Include(a => a.Publisher)
            .Include(a => a.Education)
            .ToListAsync();
        public async Task<IEnumerable<Author>> GetFilteredAsync(string? firstName, string? lastName, string? nationality)
        {
            var query = _context.Authors
                .Include(a => a.Nationality)
                .Include(a => a.Publisher)
                .Include(a => a.Education)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(firstName))
                query = query.Where(b => b.FirstName.Contains(firstName));

            if (!string.IsNullOrWhiteSpace(lastName))
                query = query.Where(b => b.LastName.Contains(lastName));

            if (!string.IsNullOrWhiteSpace(nationality))
                query = query.Where(b => b.Nationality.CountryName.Contains(nationality));

            return await query.ToListAsync();
        }
        public async Task AddAsync(Author author)
		{
			_context.Add(author);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Author author)
		{
			_context.Authors.Update(author);
			await _context.SaveChangesAsync();
		}

        public async Task DeleteAsync(int id)
        {
            var author = await _context.Authors
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);

            if (author is null) return;

            var books = await _context.Books
                .Where(b => b.AuthorId == id)
                .ToListAsync();

            foreach (var book in books)
            {
                var covers = await _context.Covers
                    .Where(c => c.BookId == book.Id)
                    .ToListAsync();

                foreach (var cover in covers)
                {
                    var artistCovers = await _context.Set<ArtistCover>()
                        .Where(ac => ac.CoverId == cover.Id)
                        .ToListAsync();

                    _context.RemoveRange(artistCovers);
                    _context.Remove(cover);
                }

                _context.Remove(book);
            }

            _context.Remove(author);
            await _context.SaveChangesAsync();
        }
		public bool AuthorExists(int id)
		{
			return _context.Authors.Any(a => a.Id == id);
		}

        public async Task<IEnumerable<Author>> GetAuthorsByNationality(string? nationality)
        {
            var query = _context.Authors
                .Include(a => a.Nationality)
                .Include(a => a.Publisher)
                .Include(a => a.Education)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(nationality))
                query = query.Where(b => b.Nationality.CountryName.Contains(nationality));

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Author>> GetByAuthorsFirstName(string? authorFirstName)
        {
            var query = _context.Authors
                .Include(a => a.Nationality)
                .Include(a => a.Publisher)
                .Include(a => a.Education)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(authorFirstName))
                query = query.Where(b => b.FirstName.Contains(authorFirstName));

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Author>> GetByAuthorsLastName(string? authorLastName)
        {
            var query = _context.Authors
                .Include(a => a.Nationality)
                .Include(a => a.Publisher)
                .Include(a => a.Education)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(authorLastName))
                query = query.Where(b => b.LastName.Contains(authorLastName));

            return await query.ToListAsync();
        }
    }
}
