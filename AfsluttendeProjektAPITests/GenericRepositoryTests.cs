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
    public class GenericRepositoryTests
    {
        private AppDbContext GetDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            return new AppDbContext(options);
        }

        private async Task SeedAwardsAsync(AppDbContext context)
        {
            var awards = new List<Award>
        {
            new Award { Id = 1, Name = "Best New Artist", DateReceived = new DateOnly(2020, 5, 1), Description = "Award for best new artist", ArtistId = 1 },
            new Award { Id = 2, Name = "Lifetime Achievement", DateReceived = new DateOnly(2022, 3, 10), ArtistId = 2 }
        };
            context.Set<Award>().AddRange(awards);
            await context.SaveChangesAsync();
        }
        private async Task SeedEditionsAsync(AppDbContext context)
        {
            var editions = new List<Edition>
        {
            new Edition { Id = 1, BookId = 1, Format = "Cool Format", ReleaseDate = new DateOnly(2022, 3, 10), Book = new Book {Id = 1, Author = new Author {Id = 1 } } },
            new Edition { Id = 2, BookId = 2,  Format = "Cooler Formatter", ReleaseDate = new DateOnly(2022, 3, 10), Book = new Book {Id = 2, Author = new Author {Id = 2 } } },
        };
            context.Set<Edition>().AddRange(editions);
            await context.SaveChangesAsync();
        }

        private async Task SeedGenresAsync(AppDbContext context)
        {
            var genres = new List<Genre>
        {
            new Genre { Id = 1, Name = "Rock" },
            new Genre { Id = 2, Name = "Jazz" }
        };
            context.Set<Genre>().AddRange(genres);
            await context.SaveChangesAsync();
        }

        private async Task SeedEducationsAsync(AppDbContext context)
        {
            var educations = new List<Education>
        {
            new Education { Id = 1, Degree = "BSc Music", Institution = "University A", GraduationYear = 2010 },
            new Education { Id = 2, Degree = "MSc Arts", Institution = "University B", GraduationYear = 2015 }
        };
            context.Set<Education>().AddRange(educations);
            await context.SaveChangesAsync();
        }

        private async Task SeedNationalitiesAsync(AppDbContext context)
        {
            var nationalities = new List<Nationality>
        {
            new Nationality { Id = 1, CountryName = "Denmark", CountryCode = "DK" },
            new Nationality { Id = 2, CountryName = "USA", CountryCode = "US" }
        };
            context.Set<Nationality>().AddRange(nationalities);
            await context.SaveChangesAsync();
        }

        private async Task SeedPublishersAsync(AppDbContext context)
        {
            var publishers = new List<Publisher>
        {
            new Publisher { Id = 1, Name = "Music House", ContactEmail = "contact@musichouse.com" },
            new Publisher { Id = 2, Name = "Sound Wave", ContactEmail = "test@test.com" }
        };
            context.Set<Publisher>().AddRange(publishers);
            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllAwards()
        {
            var context = GetDbContext(nameof(GetAllAsync_ReturnsAllAwards));
            await SeedAwardsAsync(context);
            var repo = new GenericRepository<Award>(context);

            var awards = await repo.GetAllAsync();

            Assert.Equal(2, awards.Count());
        }
        [Fact]
        public async Task GetAllAsync_ReturnsAllEditions()
        {
            var context = GetDbContext(nameof(GetAllAsync_ReturnsAllEditions));
            await SeedEditionsAsync(context);
            var repo = new GenericRepository<Edition>(context);

            var editions = await repo.GetAllAsync();

            Assert.Equal(2, editions.Count());
        }
        [Fact]
        public async Task GetAllAsync_ReturnsAllEducations()
        {
            var context = GetDbContext(nameof(GetAllAsync_ReturnsAllEducations));
            await SeedEducationsAsync(context);
            var repo = new GenericRepository<Education>(context);

            var educations = await repo.GetAllAsync();

            Assert.Equal(2, educations.Count());
        }

        [Fact]
        public async Task AddAsync_AddGenre()
        {
            var context = GetDbContext(nameof(AddAsync_AddGenre));
            await SeedGenresAsync(context);
            var repo = new GenericRepository<Genre>(context);

            var newGenre = new Genre { Id = 3, Name = "Pop" };
            await repo.AddAsync(newGenre);

            var genres = await repo.GetAllAsync();
            Assert.Equal(3, genres.Count());
            Assert.Contains(genres, g => g.Name == "Pop");
        }
        [Fact]
        public async Task AddAsync_AddNationality()
        {
            var context = GetDbContext(nameof(AddAsync_AddNationality));
            await SeedNationalitiesAsync(context);
            var repo = new GenericRepository<Nationality>(context);

            var nationality = new Nationality { Id = 3, CountryName = "cool" };
            await repo.AddAsync(nationality);

            var nationalities = await repo.GetAllAsync();
            Assert.Equal(3, nationalities.Count());
            Assert.Contains(nationalities, g => g.CountryName == "cool");
        }
        [Fact]
        public async Task AddAsync_AddPublisher()
        {
            var context = GetDbContext(nameof(AddAsync_AddPublisher));
            await SeedPublishersAsync(context);
            var repo = new GenericRepository<Publisher>(context);

            var publisher = new Publisher { Id = 3, Name = "cool pub" };
            await repo.AddAsync(publisher);

            var publishers = await repo.GetAllAsync();
            Assert.Equal(3, publishers.Count());
            Assert.Contains(publishers, g => g.Name == "cool pub");
        }

        [Fact]
        public async Task UpdateAsync_UpdateEducation()
        {
            var context = GetDbContext(nameof(UpdateAsync_UpdateEducation));
            await SeedEducationsAsync(context);
            var repo = new GenericRepository<Education>(context);

            var education = await repo.GetByIdAsync(2);
            education.Institution = "Updated University";
            await repo.UpdateAsync(education);

            var updated = await repo.GetByIdAsync(2);
            Assert.Equal("Updated University", updated.Institution);
        }
        [Fact]
        public async Task UpdateAsync_UpdateEdition()
        {
            var context = GetDbContext(nameof(UpdateAsync_UpdateEdition));
            await SeedEditionsAsync(context);
            var repo = new GenericRepository<Edition>(context);

            var edition = await repo.GetByIdAsync(2);
            edition.Format = "Updated Format";
            await repo.UpdateAsync(edition);
        
            var updated = await repo.GetByIdAsync(2);
            Assert.Equal("Updated Format", updated.Format);
        }
        [Fact]
        public async Task UpdateAsync_UpdateAward()
        {
            var context = GetDbContext(nameof(UpdateAsync_UpdateAward));
            await SeedAwardsAsync(context);
            var repo = new GenericRepository<Award>(context);

            var award = await repo.GetByIdAsync(1);
            award.Name = "Updated award";
            await repo.UpdateAsync(award);

            var updated = await repo.GetByIdAsync(1);
            Assert.Equal("Updated award", updated.Name);
        }

        [Fact]
        public async Task DeleteAsync_RemovesNationality()
        {
            var context = GetDbContext(nameof(DeleteAsync_RemovesNationality));
            await SeedNationalitiesAsync(context);
            var repo = new GenericRepository<Nationality>(context);

            await repo.DeleteAsync(1);

            var deleted = await repo.GetByIdAsync(1);
            Assert.Null(deleted);
        }
        [Fact]
        public async Task DeleteAsync_RemovesPublisher()
        {
            var context = GetDbContext(nameof(DeleteAsync_RemovesPublisher));
            await SeedPublishersAsync(context);
            var repo = new GenericRepository<Publisher>(context);

            await repo.DeleteAsync(1);

            var deleted = await repo.GetByIdAsync(1);
            Assert.Null(deleted);
        }
        [Fact]
        public async Task DeleteAsync_RemovesEdition()
        {
            var context = GetDbContext(nameof(DeleteAsync_RemovesEdition));
            await SeedEditionsAsync(context);
            var repo = new GenericRepository<Edition>(context);

            await repo.DeleteAsync(1);

            var deleted = await repo.GetByIdAsync(1);
            Assert.Null(deleted);
        }

        [Fact]
        public async Task FindAsync_FindsPublishersWithContactEmail()
        {
            var context = GetDbContext(nameof(FindAsync_FindsPublishersWithContactEmail));
            await SeedPublishersAsync(context);
            var repo = new GenericRepository<Publisher>(context);

            var publishersWithEmail = await repo.FindAsync(p => p.ContactEmail == "test@test.com");

            Assert.Single(publishersWithEmail);
            Assert.Equal("Sound Wave", publishersWithEmail.First().Name);
        }
        [Fact]
        public async Task FindAsync_FindsNationalitiesWithCountryCode()
        {
            var context = GetDbContext(nameof(FindAsync_FindsNationalitiesWithCountryCode));
            await SeedNationalitiesAsync(context);
            var repo = new GenericRepository<Nationality>(context);

            var nationalitiesWithCountryCode = await repo.FindAsync(p => p.CountryCode == "DK");

            Assert.Single(nationalitiesWithCountryCode);
            Assert.Equal("DK", nationalitiesWithCountryCode.First().CountryCode);
        }
        [Fact]
        public async Task FindAsync_FindsEducationsWithInstitutions()
        {
            var context = GetDbContext(nameof(FindAsync_FindsEducationsWithInstitutions));
            await SeedEducationsAsync(context);
            var repo = new GenericRepository<Education>(context);

            var nationalitiesWithCountryCode = await repo.FindAsync(p => p.Institution == "University A");

            Assert.Single(nationalitiesWithCountryCode);
            Assert.Equal("BSc Music", nationalitiesWithCountryCode.First().Degree);
        }
    }


}
