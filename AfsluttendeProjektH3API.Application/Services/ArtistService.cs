	using AfsluttendeProjektH3API.Application.Interfaces;
using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application.Services
{
	public class ArtistService
	{
		private readonly IArtistRepository _artistRepository;
		public ArtistService(IArtistRepository artistRepository)
		{
			_artistRepository = artistRepository;
		}
		public Task<Artist?> GetAsync(int id) => _artistRepository.GetByIdAsync(id);
        public Task<IEnumerable<Artist>> GetAllAsync(string? firstName, string? lastName, string? nationality)
    => _artistRepository.GetFilteredAsync(firstName, lastName, nationality);
        public Task<IEnumerable<Artist>> GetAllArtistsByLastNameAsync(string? lastName)
    => _artistRepository.GetByArtistLastName(lastName);
        public Task<IEnumerable<Artist>> GetAllArtistsByFirstNameAsync(string? firstName)
    => _artistRepository.GetByArtistFirstName(firstName);
        public Task<IEnumerable<Artist>> GetAllArtistsByNationalityAsync(string? nationality)
    => _artistRepository.GetArtistsByNationality(nationality);
        public Task AddAsync(Artist artist) => _artistRepository.AddAsync(artist);
		public Task UpdateAsync(Artist artist) => _artistRepository.UpdateAsync(artist);
		public Task DeleteAsync(int id) => _artistRepository.DeleteAsync(id);
	}
}
