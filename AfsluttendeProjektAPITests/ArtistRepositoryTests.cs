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
            new Artist { Id = 1, FirstName = "Test Person Banana", LastName = "Aeety", Nationality = new Nationality{ CountryName = "Potato", Id = 1}, DateOfBirth = new DateOnly(1973, 1, 1) },
            new Artist { Id = 2, FirstName = "John Adams", LastName = "Etee", Nationality = new Nationality{ CountryName = "Betete", Id = 2}, DateOfBirth = new DateOnly(1973, 1, 1) },
            new Artist { Id = 3, FirstName = "Eva Williams", LastName = "Garry", Nationality = new Nationality{ CountryName = "rampland", Id = 3}, DateOfBirth = new DateOnly(1973, 1, 1) },
            new Artist { Id = 4, FirstName = "George Davis", LastName = "Bad", Nationality = new Nationality{ CountryName = "Iceland", Id = 4},DateOfBirth = new DateOnly(1973, 1, 1) },
            new Artist { Id = 5, FirstName = "Ronal", LastName = "Dinho", Nationality = new Nationality{ CountryName = "Greenland", Id = 5}, DateOfBirth = new DateOnly(1973, 1, 1) },
            new Artist { Id = 6, FirstName = "Theis", LastName = "Gravgaard", Nationality = new Nationality{ CountryName = "USA", Id = 6}, DateOfBirth = new DateOnly(1973, 1, 1) },
            new Artist { Id = 7, FirstName = "Tee", LastName = "Meeeed", Nationality = new Nationality{ CountryName = "England", Id = 7}, DateOfBirth = new DateOnly(1972, 1, 1) }
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

        [Theory]
        [InlineData("Nonexistent")]
        [InlineData("Unknown")]
        [InlineData("Narnia")]
        [InlineData("MiddleEarth")]
        public async Task GetByNationalityAsync_ReturnsEmptyArray_WhenNationalityDoesNotExist(string nationality)
        {
            var context = GetDbContext(nameof(GetByNationalityAsync_ReturnsEmptyArray_WhenNationalityDoesNotExist) + nationality);
            await SeedAsync(context);
            var repo = new ArtistRepository(context);

            var result = await repo.GetArtistsByNationality(nationality);

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllArtists()
        {
            var context = GetDbContext(nameof(GetAllAsync_ReturnsAllArtists));
            await SeedAsync(context);
            var repo = new ArtistRepository(context);

            var artists = await repo.GetAllAsync();

            Assert.Equal(7, artists.Count());
        }

        [Theory]
        [InlineData("Aeety", 1)]
        [InlineData("Dinho", 1)]
        [InlineData("Nonexistent", 0)]
        public async Task GetByLastName_FiltersCorrectly(string lastName, int expectedCount)
        {
            var context = GetDbContext(nameof(GetByLastName_FiltersCorrectly) + lastName);
            await SeedAsync(context);
            var repo = new ArtistRepository(context);

            var artists = await repo.GetByArtistLastName(lastName);

            Assert.Equal(expectedCount, artists.Count());
        }


        [Theory]
        [InlineData("Eva Williams", "Garry", 1)]
        [InlineData("John", "etee", 1)]
        [InlineData("Nonexistent", "lastname", 0)]
        [InlineData("George", "bad", 1)]
        public async Task GetFilteredAsync_FiltersCorrectlyWhenNationalityIsNull(string firstName, string lastName, int expectedCount)
        {
            var context = GetDbContext(nameof(GetFilteredAsync_FiltersCorrectlyWhenNationalityIsNull) + firstName + lastName);
            await SeedAsync(context);
            var repo = new ArtistRepository(context);

            var artists = await repo.GetFilteredAsync(firstName, lastName, null);

            Assert.Equal(expectedCount, artists.Count());
        }
        [Theory]
        [InlineData("Ronal", "Greenland", 1)]
        [InlineData("John Adams", "Betete", 1)]
        [InlineData("Nonexistent", "nowhere", 0)]
        [InlineData("Tee", "England", 1)]
        public async Task GetFilteredAsync_FiltersCorrectlyWhenLastNameIsNull(string firstName, string nationality, int expectedCount)
        {
            var context = GetDbContext(nameof(GetFilteredAsync_FiltersCorrectlyWhenLastNameIsNull) + firstName + nationality);
            await SeedAsync(context);
            var repo = new ArtistRepository(context);

            var artists = await repo.GetFilteredAsync(firstName, null, nationality);

            Assert.Equal(expectedCount, artists.Count());
        }
        [Theory]
        [InlineData("Test Person Banana", 1)]
        [InlineData("Banana", 0)]         
        [InlineData("John Adams", 1)]        
        [InlineData("Eva Williams", 1)]       
        [InlineData("Nonexistent", 0)]
        public async Task GetByFirstName_FiltersCorrectly(string firstName, int expectedCount)
        {
            var context = GetDbContext(nameof(GetByFirstName_FiltersCorrectly) + firstName);
            await SeedAsync(context);
            var repo = new ArtistRepository(context);

            var artists = await repo.GetByArtistFirstName(firstName);

            Assert.Equal(expectedCount, artists.Count());
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

            Assert.Equal(8, context.Artists.Count());
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

        [Theory]
        [InlineData(1, true)]
        [InlineData(4, true)]
        [InlineData(7, true)]
        [InlineData(4444, false)]
        [InlineData(54, false)]
        public async Task ArtistExists_ReturnsCorrectResult(int artistId, bool expectedResult)
        {
            var context = GetDbContext(nameof(ArtistExists_ReturnsCorrectResult) + artistId);
            await SeedAsync(context);
            context.SaveChanges();

            var repo = new ArtistRepository(context);

            var result = repo.ArtistExists(artistId);

            Assert.Equal(expectedResult, result);
        }
    }
}
