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
    public class ArtistRepositoryTests
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
            var artists = new List<Artist>
        {
            new Artist { Id = 1, FirstName = "Test Person Banana", LastName = "Simons", DateOfBirth = new DateOnly(1973, 1, 1) },
            new Artist { Id = 2, FirstName = "Test Person 2", LastName = "Styles", DateOfBirth = new DateOnly(1972, 1, 1) }
        };

            context.Artists.AddRange(artists);
            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsArtist_WhenIdExists()
        {
            var dbName = nameof(GetByIdAsync_ReturnsArtist_WhenIdExists);
            var context = GetDbContext(dbName);
            await SeedAsync(context);

            var repo = new ArtistRepository(context);

            var artist = await repo.GetByIdAsync(1);

            Assert.NotNull(artist);
            Assert.Equal("Test Person Banana", artist.FirstName);
        }

        [Fact]
        public async Task GetByNationalityAsync_ReturnsEmptyArray_WhenNationalityDoesNotExist()
        {
            var context = GetDbContext(nameof(GetByNationalityAsync_ReturnsEmptyArray_WhenNationalityDoesNotExist));
            await SeedAsync(context);
            var repo = new ArtistRepository(context);

            var result = await repo.GetArtistsByNationality("Nonexistent");

            Assert.Equal([], result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllArtists()
        {
            var context = GetDbContext(nameof(GetAllAsync_ReturnsAllArtists));
            await SeedAsync(context);
            var repo = new ArtistRepository(context);

            var artists = await repo.GetAllAsync();

            Assert.Equal(2, artists.Count());
        }

        [Fact]
        public async Task GetByLastName_FiltersCorrectly()
        {
            var context = GetDbContext(nameof(GetByLastName_FiltersCorrectly));
            await SeedAsync(context);
            var repo = new ArtistRepository(context);

            var artists = await repo.GetByArtistLastName("Styles");

            Assert.Equal(1, artists.Count());
        }
        [Fact]
        public async Task GetFilteredAsync_FiltersCorrectlyWhenNationalityIsNull()
        {
            var context = GetDbContext(nameof(GetFilteredAsync_FiltersCorrectlyWhenNationalityIsNull));
            await SeedAsync(context);
            var repo = new ArtistRepository(context);

            var artists = await repo.GetFilteredAsync("Test person banana", "simons", null);

            Assert.Equal(1, artists.Count());
        }
        [Fact]
        public async Task GetFilteredAsync_FiltersCorrectlyWhenLastNameIsNull()
        {
            var context = GetDbContext(nameof(GetFilteredAsync_FiltersCorrectlyWhenLastNameIsNull));
            await SeedAsync(context);
            var repo = new ArtistRepository(context);

            var artists = await repo.GetFilteredAsync("Test person banana", null, "greenlandic");

            Assert.Equal(1, artists.Count());
        }
        [Fact]
        public async Task GetByFirstName_FiltersCorrectly()
        {
            var context = GetDbContext(nameof(GetByFirstName_FiltersCorrectly));
            await SeedAsync(context);
            var repo = new ArtistRepository(context);

            var artists = await repo.GetByArtistFirstName("Test Person banana");

            Assert.Equal(1, artists.Count());
        }

        [Fact]
        public async Task AddAsync_AddsArtistSuccessfully()
        {
            var context = GetDbContext(nameof(AddAsync_AddsArtistSuccessfully));
            await SeedAsync(context);
            var repo = new ArtistRepository(context);

            var artist = new Artist { Id = 42, FirstName = "Børge", LastName = "Coolman" };
            context.Artists.Add(artist);
            await context.SaveChangesAsync();

            Assert.Equal(3, context.Artists.Count());
        }

        [Fact]
        public async Task DeleteAsync_RemovesArtist_WhenExists()
        {
            var context = GetDbContext(nameof(DeleteAsync_RemovesArtist_WhenExists));
            await SeedAsync(context);
            var repo = new ArtistRepository(context);

            await repo.DeleteAsync(1);

            var artist = await context.Artists.FindAsync(1);
            Assert.Null(artist);
        }

        [Fact]
        public void AuthorExists_ReturnsTrue_WhenArtistExists()
        {
            var context = GetDbContext(nameof(AuthorExists_ReturnsTrue_WhenArtistExists));
            context.Artists.Add(new Artist { Id = 42, FirstName = "Hans", LastName = "Egonsen", DateOfBirth = new DateOnly(1972, 1, 1) });
            context.SaveChanges();

            var repo = new ArtistRepository(context);

            Assert.True(repo.ArtistExists(42));
        }
    }
}
