using AfsluttendeProjektH3API.Domain.Entities;
using AfsluttendeProjektH3API.Infrastructure.Repositories;
using AfsluttendeProjektH3API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfsluttendeProjektAPITests
{
    public class AuthorRepositoryTests
    {
        private AppDbContext GetDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(dbName)
            .Options;

            return new AppDbContext(options);
        }

        private async Task SeedAsync(AppDbContext context)
        {
            var authors = new List<Author>
        {
            new Author { Id = 1, FirstName = "Test Person 1", LastName = "John", LastPublishedBook = "Last Publish Test", DateOfBirth = new DateOnly(1970, 1, 1), NumberOfBooksPublished = 200 },
            new Author { Id = 2, FirstName = "Test Person 2", LastName = "Egon",  LastPublishedBook = "Last Publish Test 2", Nationality = "Japanese", DateOfBirth = new DateOnly(1972, 1, 1), NumberOfBooksPublished = 300}
        };

            context.Authors.AddRange(authors);
            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsAuthor_WhenIdExists()
        {
            var dbName = nameof(GetByIdAsync_ReturnsAuthor_WhenIdExists);
            var context = GetDbContext(dbName);
            await SeedAsync(context);

            var repo = new AuthorRepository(context);

            var author = await repo.GetByIdAsync(1);

            Assert.NotNull(author);
            Assert.Equal("Test Person 1", author.FirstName);
        }

        [Fact]
        public async Task GetByNationalityAsync_ReturnsEmptyArray_WhenNationalityDoesNotExist()
        {
            var context = GetDbContext(nameof(GetByNationalityAsync_ReturnsEmptyArray_WhenNationalityDoesNotExist));
            await SeedAsync(context);
            var repo = new AuthorRepository(context);

            var result = await repo.GetAuthorsByNationality("Nonexistent");

            Assert.Equal([],result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllAuthors()
        {
            var context = GetDbContext(nameof(GetAllAsync_ReturnsAllAuthors));
            await SeedAsync(context);
            var repo = new AuthorRepository(context);

            var authors = await repo.GetAllAsync();

            Assert.Equal(2, authors.Count());
        }

        [Fact]
        public async Task GetByLastName_FiltersCorrectly()
        {
            var context = GetDbContext(nameof(GetByLastName_FiltersCorrectly));
            await SeedAsync(context);
            var repo = new AuthorRepository(context);

            var artists = await repo.GetByAuthorsLastName("John");

            Assert.Equal(1, artists.Count());
        }

        [Fact]
        public async Task GetByFirstName_FiltersCorrectly()
        {
            var context = GetDbContext(nameof(GetByFirstName_FiltersCorrectly));
            await SeedAsync(context);
            var repo = new AuthorRepository(context);

            var artists = await repo.GetByAuthorsFirstName("Test Person 2");

            Assert.Equal(1, artists.Count());
        }

        [Fact]
        public async Task AddAsync_AddsAuthorSuccessfully()
        {
            var context = GetDbContext(nameof(AddAsync_AddsAuthorSuccessfully));
            await SeedAsync(context);
            var repo = new AuthorRepository(context);

            var author = new Author { Id = 42, FirstName = "Theis", LastName = "Coolman" };
            context.Authors.Add(author);
            await context.SaveChangesAsync();

            Assert.Equal(3, context.Authors.Count());
        }

        [Fact]
        public async Task DeleteAsync_RemovesAuthor_WhenExists()
        {
            var context = GetDbContext(nameof(DeleteAsync_RemovesAuthor_WhenExists));
            await SeedAsync(context);
            var repo = new AuthorRepository(context);

            await repo.DeleteAsync(1);

            var author = await context.Authors.FindAsync(1);
            Assert.Null(author);
        }

        [Fact]
        public void AuthorExists_ReturnsTrue_WhenAuthorExists()
        {
            var context = GetDbContext(nameof(AuthorExists_ReturnsTrue_WhenAuthorExists));
            context.Authors.Add(new Author { Id = 42, FirstName = "Peter", LastName = "Steven", DateOfBirth = new DateOnly(1970, 1, 1) });
            context.SaveChanges();

            var repo = new AuthorRepository(context);

            Assert.True(repo.AuthorExists(42));
        }
    }
}
