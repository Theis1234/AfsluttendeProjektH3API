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
			await _context.Authors.FirstOrDefaultAsync(p => p.Id == id);

		public async Task<IEnumerable<Author>> GetAllAsync() =>
			await _context.Authors.ToListAsync();
        public async Task<IEnumerable<Author>> GetFilteredAsync(string? firstName, string? lastName, string? nationality)
        {
            var query = _context.Authors
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(firstName))
                query = query.Where(b => b.FirstName.Contains(firstName));

            if (!string.IsNullOrWhiteSpace(lastName))
                query = query.Where(b => b.LastName.Contains(lastName));

            if (!string.IsNullOrWhiteSpace(nationality))
                query = query.Where(b => b.Nationality.Contains(nationality));

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
			var entity = await _context.Authors.FindAsync(id);
			if (entity != null)
			{
				_context.Authors.Remove(entity);
				await _context.SaveChangesAsync();
			}
		}

		public bool AuthorExists(int id)
		{
			return _context.Authors.Any(a => a.Id == id);
		}

    }
}
