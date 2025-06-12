using AfsluttendeProjektH3API.Application.Interfaces;
using AfsluttendeProjektH3API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AfsluttendeProjektH3API.Infrastructure.Repositories
{
	public class BookRepository : IBookRepository
	{
		private readonly AppDbContext _context;
		public BookRepository(AppDbContext context) => _context = context;

		public async Task<Book?> GetByIdAsync(int id) =>
			await _context.Books.FirstOrDefaultAsync(p => p.Id == id);

		public async Task<IEnumerable<Book>> GetAllAsync() =>
			await _context.Books.Include(b => b.Author).ToListAsync();

		public async Task AddAsync(Book book)
		{
			_context.Add(book);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Book book)
		{
			_context.Books.Update(book);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await _context.Books.FindAsync(id);
			if (entity != null)
			{
				_context.Books.Remove(entity);
				await _context.SaveChangesAsync();
			}
		}
		public bool BookExists(int id)
		{
			return _context.Books.Any(a => a.Id == id);
		}
	}
}
