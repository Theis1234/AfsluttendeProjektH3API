using AfsluttendeProjektH3API.Application.Interfaces;
using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public Task<Author?> GetAsync(int id) => _authorRepository.GetByIdAsync(id);
        public Task<IEnumerable<Author>> GetAllAsync(string? firstName, string? lastName, string? nationality)
    => _authorRepository.GetFilteredAsync(firstName, lastName, nationality);
        public Task<IEnumerable<Author>> GetAllAuthorsByLastNameAsync(string? lastName)
   => _authorRepository.GetByAuthorsLastName(lastName);
        public Task<IEnumerable<Author>> GetAllAuthorsByFirstNameAsync(string? firstName)
    => _authorRepository.GetByAuthorsFirstName(firstName);
        public Task<IEnumerable<Author>> GetAllAuthorsByNationalityAsync(string? nationality)
    => _authorRepository.GetAuthorsByNationality(nationality);
        public Task AddAsync(Author author) => _authorRepository.AddAsync(author);
        public Task UpdateAsync(Author author) => _authorRepository.UpdateAsync(author);
        public Task DeleteAsync(int id) => _authorRepository.DeleteAsync(id);
    }
}
