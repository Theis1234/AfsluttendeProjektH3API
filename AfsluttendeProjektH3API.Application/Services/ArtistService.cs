	using AfsluttendeProjektH3API.Application.Interfaces;
using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;
        private readonly INationalityService _nationalityService;
        private readonly IAwardService _awardService;
        public ArtistService(IArtistRepository artistRepository, INationalityService nationalityService, IAwardService awardService)
        {
            _artistRepository = artistRepository;
            _nationalityService = nationalityService;
            _awardService = awardService;
        }
        public async Task<Artist?> GetAsync(int id)
        {
            var artist = await _artistRepository.GetByIdAsync(id);

            if (artist != null)
            {
                artist.Nationality = await _nationalityService.GetAsync(artist.NationalityId);
                var awards = await _awardService.GetAwardsByArtistIdAsync(artist.Id);
                artist.Awards = awards.ToList();
            }

            return artist;
        }
        public async Task<IEnumerable<Artist>> GetAllAsync(string? firstName, string? lastName, string? nationality)
        {
            var artists = await _artistRepository.GetFilteredAsync(firstName, lastName, nationality);

            foreach (var artist in artists)
            {
                artist.Nationality = await _nationalityService.GetAsync(artist.NationalityId);
                var awards = await _awardService.GetAwardsByArtistIdAsync(artist.Id);
                artist.Awards = awards.ToList();
            }

            return artists;
        }

        public async Task<IEnumerable<Artist>> GetAllArtistsByLastNameAsync(string? lastName)
        {
            var artists = await _artistRepository.GetByArtistLastName(lastName);

            foreach (var artist in artists)
            {
                artist.Nationality = await _nationalityService.GetAsync(artist.NationalityId);
                var awards = await _awardService.GetAwardsByArtistIdAsync(artist.Id);
                artist.Awards = awards.ToList();
            }

            return artists;
        }
        public async Task<IEnumerable<Artist>> GetAllArtistsByFirstNameAsync(string? firstName)
        {
            var artists = await _artistRepository.GetByArtistFirstName(firstName);

            foreach (var artist in artists)
            {
                artist.Nationality = await _nationalityService.GetAsync(artist.NationalityId);
                var awards = await _awardService.GetAwardsByArtistIdAsync(artist.Id);
                artist.Awards = awards.ToList();
            }

            return artists;
        }
        public Task<IEnumerable<Artist>> GetAllArtistsByNationalityAsync(string? nationality)
    => _artistRepository.GetArtistsByNationality(nationality);
        public Task AddAsync(Artist artist) => _artistRepository.AddAsync(artist);
        public Task UpdateAsync(Artist artist) => _artistRepository.UpdateAsync(artist);
        public Task DeleteAsync(int id) => _artistRepository.DeleteAsync(id);
    }
}
