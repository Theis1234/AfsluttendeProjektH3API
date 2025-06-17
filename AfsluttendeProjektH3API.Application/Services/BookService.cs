using AfsluttendeProjektH3API.Application.Interfaces;
using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application.Services
{
	public class BookService
	{
		private readonly IBookRepository _bookRepository;
		public BookService(IBookRepository bookRepository)
		{
			_bookRepository = bookRepository;
		}
		public Task<Book?> GetAsync(int id) => _bookRepository.GetByIdAsync(id);
        public Task<IEnumerable<Book>> GetAllAsync(string? title, string? genre, string? authorName)
    => _bookRepository.GetFilteredAsync(title, genre, authorName);
        public Task AddAsync(Book book) => _bookRepository.AddAsync(book);
		public Task UpdateAsync(Book book) => _bookRepository.UpdateAsync(book);
		public Task DeleteAsync(int id) => _bookRepository.DeleteAsync(id);
	}
}
