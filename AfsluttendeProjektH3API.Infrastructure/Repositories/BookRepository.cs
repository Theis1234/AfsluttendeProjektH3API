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
			await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(p => p.Id == id);

        public async Task<Book?> GetByTitleAsync(string title) =>
            await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(p => p.Title == title);

        public async Task<IEnumerable<Book>> GetAllAsync() =>
			await _context.Books.Include(b => b.Author).ToListAsync();

        public async Task<IEnumerable<Book>> GetByAuthorLastName(string? authorLastName)
        {
            var query = _context.Books
                .Include(b => b.Author)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(authorLastName))
                query = query.Where(b => b.Author.LastName.Contains(authorLastName));

            return await query.ToListAsync();
        }


        public async Task<IEnumerable<Book>> GetFilteredAsync(string? title, string? genre, string? authorName)
        {
            var query = _context.Books
                .Include(b => b.Author)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(b => b.Title.Contains(title));

            if (!string.IsNullOrWhiteSpace(genre))
                query = query.Where(b => b.Genre.Contains(genre));

            if (!string.IsNullOrWhiteSpace(authorName))
            {
                var lowerName = authorName.ToLower();
                query = query.Where(b =>
                    (b.Author.FirstName + " " + b.Author.LastName).ToLower().Contains(lowerName)
                );
            }

            return await query.ToListAsync();
        }

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
