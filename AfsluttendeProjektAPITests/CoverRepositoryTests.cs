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
    public class CoverRepositoryTests
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
            var books = new List<Book>
            {
                new Book { Id = 4,
                Title = "Some Book",
                PublishedDate = new DateOnly(2000, 1, 1),
                NumberOfPages = 123,
                BasePrice = 9.99,
                Author = new Author
                {
                    Id = 10,
                    FirstName = "Test Person 1",
                    LastName = "Author",
                    DateOfBirth = new DateOnly(1980, 1, 1)                    
                } },
                new Book { Id = 1,
                Title = "Some Book 2",
                PublishedDate = new DateOnly(1977, 1, 1),
                NumberOfPages = 123,
                BasePrice = 9.99,
                Author = new Author
                {
                    Id = 5,
                    FirstName = "Test Person 2",
                    LastName = "Author",
                    DateOfBirth = new DateOnly(1980, 1, 1)
                } },
                new Book { Id = 3,
                Title = "Some Boo 3k",
                PublishedDate = new DateOnly(1976, 1, 1),
                NumberOfPages = 123,
                BasePrice = 9.99,
                Author = new Author
                {
                    Id = 1,
                    FirstName = "Test Person 3",
                    LastName = "Author",
                    DateOfBirth = new DateOnly(1980, 1, 1)
                } 
                }
            };
     
            context.Books.AddRange(books);

            var covers = new List<Cover>
            {
                new Cover { Id = 1, Title = "Bananatitle", DigitalOnly = true, BookId = 4 },
                new Cover { Id = 2, Title = "Coooool Cover", DigitalOnly = false, BookId = 1 },
                new Cover { Id = 5, Title = "Very Nice Cover", DigitalOnly = false, BookId = 3 }
            };

            context.Covers.AddRange(covers);
            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCover_WhenIdExists()
        {
            var dbName = nameof(GetByIdAsync_ReturnsCover_WhenIdExists);
            var context = GetDbContext(dbName);
            await SeedAsync(context);

            var repo = new CoverRepository(context);

            var cover = await repo.GetByIdAsync(1);

            Assert.NotNull(cover);
            Assert.Equal("Bananatitle", cover!.Title);
        }
        [Fact]
        public async Task GetCoverByBookIdAsync_ReturnsCover()
        {
            var context = GetDbContext(nameof(GetCoverByBookIdAsync_ReturnsCover));
            await SeedAsync(context);
            var repo = new CoverRepository(context);

            var result = await repo.GetCoverByBookIdAsync(1);

            Assert.Equal(2, result.Id);
        }
        [Fact]
        public async Task GetCoverByBookIdAsync_ReturnsNull_WhenBookIdDoesNotExist()
        {
            var context = GetDbContext(nameof(GetCoverByBookIdAsync_ReturnsNull_WhenBookIdDoesNotExist));
            await SeedAsync(context);
            var repo = new CoverRepository(context);

            var result = await repo.GetCoverByBookIdAsync(5);

            Assert.Null(result);
        }
        [Fact]
        public async Task GetDigitalOnlyCoversAsync_ReturnsDigitalOnlyCovers()
        {
            var context = GetDbContext(nameof(GetDigitalOnlyCoversAsync_ReturnsDigitalOnlyCovers));
            await SeedAsync(context);
            var repo = new CoverRepository(context);

            var result = await repo.GetDigitalOnlyCoversAsync(true);

            Assert.True(result.All(c => c.DigitalOnly == true));
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllCovers()
        {
            var context = GetDbContext(nameof(GetAllAsync_ReturnsAllCovers));
            await SeedAsync(context);
            var repo = new CoverRepository(context);

            var covers = await repo.GetAllAsync();

            Assert.Equal(3, covers.Count());
        }


        [Fact]
        public async Task GetFilteredAsync_ReturnsFilteredCovers()
        {
            var context = GetDbContext(nameof(GetFilteredAsync_ReturnsFilteredCovers));
            await SeedAsync(context);
            var repo = new CoverRepository(context);

            var books = await repo.GetFilteredAsync("Bananatitle", true);

            Assert.Single(books);
            Assert.Equal("Bananatitle", books.First().Title);
        }

        [Fact]
        public async Task AddAsync_AddsCoverSuccessfully()
        {
            var context = GetDbContext(nameof(AddAsync_AddsCoverSuccessfully));
            await SeedAsync(context);
            var repo = new CoverRepository(context);

            var author = new Author { Id = 2, FirstName = "Theis", LastName = "Coolman" };
            context.Authors.Add(author);
            await context.SaveChangesAsync();

            var newBook = new Book
            {
                Title = "Test Book 3",
                AuthorId = 2,
                Author = author,
                PublishedDate = new DateOnly(2023, 1, 1),
                NumberOfPages = 150,
                BasePrice = 12
            };
            context.Books.Add(newBook);
            await context.SaveChangesAsync();

            var newCover = new Cover
            {
                Title = "Test Book 3",
                DigitalOnly = true,
                Book = newBook
            };

            await repo.AddAsync(newCover);

            Assert.Equal(4, context.Covers.Count());
        }

        [Fact]
        public async Task DeleteAsync_RemovesCover_WhenExists()
        {
            var context = GetDbContext(nameof(DeleteAsync_RemovesCover_WhenExists));
            await SeedAsync(context);
            var repo = new CoverRepository(context);

            await repo.DeleteAsync(1);

            var book = await context.Covers.FindAsync(1);
            Assert.Null(book);
        }

        [Fact]
        public void BookExists_ReturnsTrue_WhenCoverExists()
        {
            var context = GetDbContext(nameof(BookExists_ReturnsTrue_WhenCoverExists));
            context.Covers.Add(new Cover { Id = 42, Title = "Exists" });
            context.SaveChanges();

            var repo = new CoverRepository(context);

            Assert.True(repo.CoverExists(42));
        }
    }
}
