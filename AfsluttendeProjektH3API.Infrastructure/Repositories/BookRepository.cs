using AfsluttendeProjektH3API.Application.Interfaces;
using AfsluttendeProjektH3API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AfsluttendeProjektH3API.Infrastructure.Repositories
{
	public class BookRepository : IBookRepository
	{
		private readonly AppDbContext _context;
		public BookRepository(AppDbContext context) => _context = context;

		public async Task<Book?> GetByIdAsync(int id) =>
			await _context.Books.Include(b => b.Author).Include(a => a.Genre)
            .Include(a => a.Editions).FirstOrDefaultAsync(p => p.Id == id);

        public async Task<Book?> GetByTitleAsync(string title) =>
            await _context.Books.Include(b => b.Author).Include(a => a.Genre)
            .Include(a => a.Editions).FirstOrDefaultAsync(p => p.Title == title);

        public async Task<IEnumerable<Book>> GetAllAsync() =>
			await _context.Books.Include(b => b.Author).Include(a => a.Genre)
            .Include(a => a.Editions).ToListAsync();

        public async Task<IEnumerable<Book>> GetByAuthorLastName(string? authorLastName)
        {
            var query = _context.Books
                .Include(b => b.Author).Include(a => a.Genre)
                .Include(a => a.Editions)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(authorLastName))
                query = query.Where(b => b.Author.LastName.Contains(authorLastName));

            return await query.ToListAsync();
        }


        public async Task<IEnumerable<Book>> GetFilteredAsync(string? title, string? genre, string? authorName)
        {
            var query = _context.Books
                .Include(b => b.Author).Include(a => a.Genre)
            .Include(a => a.Editions)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(b => b.Title.Contains(title));

            if (!string.IsNullOrWhiteSpace(genre))
                query = query.Where(b => b.Genre.Name.Contains(genre));

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
            foreach (var edition in book.Editions)
            {
                edition.Book = book;
            }
            _context.Books.Update(book);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await _context.Books.FindAsync(id);
			if (entity != null)
			{
                var coverIds = await _context.Covers.Where(c => c.BookId == id).Select(c => c.Id).ToListAsync();

                var artistCovers = _context.ArtistCovers.Where(ac => coverIds.Contains(ac.CoverId));
                _context.ArtistCovers.RemoveRange(artistCovers);

                var covers = _context.Covers.Where(c => c.BookId == id);
                _context.Covers.RemoveRange(covers);

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
