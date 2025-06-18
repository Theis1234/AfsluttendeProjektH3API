using AfsluttendeProjektH3API.Domain.Entities;
using AfsluttendeProjektH3API.Infrastructure;
using AfsluttendeProjektH3API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace AfsluttendeProjektH3APITests;

public class BookRepositoryTests
{
    private AppDbContext GetDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
        .Options;

        return new AppDbContext(options);
    }

    private async Task SeedAsync(AppDbContext context)
    {
        var author = new Author { Id = 1, FirstName = "John", LastName = "Doe" };
        var books = new List<Book>
        {
            new Book { Id = 1, Title = "Book A", Genre = "Fiction", Author = author, AuthorId = author.Id, PublishedDate = new DateOnly(2020, 1, 1), NumberOfPages = 200, BasePrice = 10 },
            new Book { Id = 2, Title = "Book B", Genre = "Sci-Fi", Author = author, AuthorId = author.Id, PublishedDate = new DateOnly(2021, 1, 1), NumberOfPages = 300, BasePrice = 15 }
        };

        context.Authors.Add(author);
        context.Books.AddRange(books);
        await context.SaveChangesAsync();
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsBook_WhenIdExists()
    {
        // Arrange
        var dbName = nameof(GetByIdAsync_ReturnsBook_WhenIdExists);
        var context = GetDbContext(dbName);
        await SeedAsync(context);

        var repo = new BookRepository(context);

        // Act
        var book = await repo.GetByIdAsync(1);

        // Assert
        Assert.NotNull(book);
        Assert.Equals("Book A", book!.Title);
    }

    [Fact]
    public async Task GetByTitleAsync_ReturnsNull_WhenTitleDoesNotExist()
    {
        var context = GetDbContext(nameof(GetByTitleAsync_ReturnsNull_WhenTitleDoesNotExist));
        await SeedAsync(context);
        var repo = new BookRepository(context);

        var result = await repo.GetByTitleAsync("Nonexistent");

        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllBooks()
    {
        var context = GetDbContext(nameof(GetAllAsync_ReturnsAllBooks));
        await SeedAsync(context);
        var repo = new BookRepository(context);

        var books = await repo.GetAllAsync();

        Assert.Equal(2, books.Count());
    }

    [Fact]
    public async Task GetByAuthorLastName_FiltersCorrectly()
    {
        var context = GetDbContext(nameof(GetByAuthorLastName_FiltersCorrectly));
        await SeedAsync(context);
        var repo = new BookRepository(context);

        var books = await repo.GetByAuthorLastName("Doe");

        Assert.Equal(2, books.Count());
    }

    [Fact]
    public async Task GetFilteredAsync_ReturnsFilteredBooks_ByTitle()
    {
        var context = GetDbContext(nameof(GetFilteredAsync_ReturnsFilteredBooks_ByTitle));
        await SeedAsync(context);
        var repo = new BookRepository(context);

        var books = await repo.GetFilteredAsync("Book A", null, null);

        Assert.Single(books);
        Assert.Equal("Book A", books.First().Title);
    }

    [Fact]
    public async Task AddAsync_AddsBookSuccessfully()
    {
        var context = GetDbContext(nameof(AddAsync_AddsBookSuccessfully));
        var repo = new BookRepository(context);

        var author = new Author { Id = 1, FirstName = "Jane", LastName = "Smith" };
        context.Authors.Add(author);
        await context.SaveChangesAsync();

        var newBook = new Book
        {
            Title = "New Book",
            Genre = "Drama",
            AuthorId = 1,
            Author = author,
            PublishedDate = new DateOnly(2023, 1, 1),
            NumberOfPages = 150,
            BasePrice = 12
        };

        await repo.AddAsync(newBook);

        Assert.Equal(1, context.Books.Count());
    }

    [Fact]
    public async Task DeleteAsync_RemovesBook_WhenExists()
    {
        var context = GetDbContext(nameof(DeleteAsync_RemovesBook_WhenExists));
        await SeedAsync(context);
        var repo = new BookRepository(context);

        await repo.DeleteAsync(1);

        var book = await context.Books.FindAsync(1);
        Assert.Null(book);
    }

    [Fact]
    public void BookExists_ReturnsTrue_WhenBookExists()
    {
        var context = GetDbContext(nameof(BookExists_ReturnsTrue_WhenBookExists));
        context.Books.Add(new Book { Id = 42, Title = "Exists", AuthorId = 1, Author = new Author(), PublishedDate = new DateOnly(2022, 1, 1), NumberOfPages = 100, BasePrice = 20 });
        context.SaveChanges();

        var repo = new BookRepository(context);

        Assert.True(repo.BookExists(42));
    }
}
