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
