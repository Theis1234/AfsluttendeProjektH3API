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
		public Task<IEnumerable<Artist>> GetAllAsync() => _artistRepository.GetAllAsync();
		public Task AddAsync(Artist artist) => _artistRepository.AddAsync(artist);
		public Task UpdateAsync(Artist artist) => _artistRepository.UpdateAsync(artist);
		public Task DeleteAsync(int id) => _artistRepository.DeleteAsync(id);
	}
}
