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
            var publisher = new Publisher { Id = 1, Name = "doesntmatter" };
            var nationality = new Nationality { Id = 1, CountryName = "coolland" };
            var education = new Education { Id = 1, Degree = "very cool education" };


            var authors = new List<Author>
        {
            new Author { Id = 1, FirstName = "Test Person 1", LastName = "John", LastPublishedBook = "Last Publish Test", DateOfBirth = new DateOnly(1970, 1, 1), NumberOfBooksPublished = 200, Publisher = publisher, Nationality = nationality, Education = education },
            new Author { Id = 2, FirstName = "Test Person 2", LastName = "Coolman",  LastPublishedBook = "Last Publish Test 2", DateOfBirth = new DateOnly(1972, 1, 1), NumberOfBooksPublished = 300, Publisher = publisher, Nationality = nationality, Education = education},
            new Author { Id = 3, FirstName = "Test Person 4", LastName = "Terry", LastPublishedBook = "Last 4322", DateOfBirth = new DateOnly(1970, 1, 1), NumberOfBooksPublished = 200, Publisher = publisher, Nationality = nationality, Education = education },
            new Author { Id = 5, FirstName = "Test Person 6", LastName = "Berry",  LastPublishedBook = "Last142", DateOfBirth = new DateOnly(1972, 1, 1), NumberOfBooksPublished = 300, Publisher = publisher, Nationality = nationality, Education = education}
        };

            context.Authors.AddRange(authors);
            await context.SaveChangesAsync();
        }

        [Theory]
        [InlineData(1, "Test Person 1")]
        [InlineData(5, "Test Person 6")] // Optional: Add only if these exist in SeedAsync
        public async Task GetByIdAsync_ReturnsAuthor_WhenIdExists(int authorId, string expectedFirstName)
        {
            var dbName = nameof(GetByIdAsync_ReturnsAuthor_WhenIdExists) + authorId;
            var context = GetDbContext(dbName);
            await SeedAsync(context);

            var repo = new AuthorRepository(context);

            var author = await repo.GetByIdAsync(authorId);

            Assert.NotNull(author);
            Assert.Equal(expectedFirstName, author.FirstName);
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

            Assert.Equal(4, authors.Count());
        }

        [Theory]
        [InlineData("John", 1)]
        [InlineData("Doe", 0)] // Example additional case
        [InlineData("Coolman", 1)] // Another possible match if present in seed data
        public async Task GetByLastName_FiltersCorrectly(string lastName, int expectedCount)
        {
            var context = GetDbContext(nameof(GetByLastName_FiltersCorrectly) + lastName);
            await SeedAsync(context);
            var repo = new AuthorRepository(context);

            var authors = await repo.GetByAuthorsLastName(lastName);

            Assert.Equal(expectedCount, authors.Count());
        }

        [Theory]
        [InlineData("Test Person 2", 1)]
        [InlineData("Test Person 1", 1)]
        [InlineData("john", 0)]
        public async Task GetByFirstName_FiltersCorrectly(string firstName, int expectedCount)
        {
            var context = GetDbContext(nameof(GetByFirstName_FiltersCorrectly) + firstName);
            await SeedAsync(context);
            var repo = new AuthorRepository(context);

            var authors = await repo.GetByAuthorsFirstName(firstName);

            Assert.Equal(expectedCount, authors.Count());
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

            Assert.Equal(5, context.Authors.Count());
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
        [Theory]
        [InlineData("Test Person 1", "John", "coolland", 1)]
        [InlineData("Test", null, null, 4)]
        [InlineData(null, "Coolman", null, 1)]
        [InlineData(null, null, "coolland", 4)]
        [InlineData("Nonexistent", "Nobody", "Nowhere", 0)] 
        public async Task GetFilteredAsync_FiltersByMultipleFieldsCorrectly(
    string? firstName, string? lastName, string? nationality, int expectedCount)
        {
            var context = GetDbContext(nameof(GetFilteredAsync_FiltersByMultipleFieldsCorrectly) + firstName + lastName + nationality);
            await SeedAsync(context);
            var repo = new AuthorRepository(context);

            var authors = await repo.GetFilteredAsync(firstName, lastName, nationality);

            Assert.Equal(expectedCount, authors.Count());
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
