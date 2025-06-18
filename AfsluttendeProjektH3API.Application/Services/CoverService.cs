using AfsluttendeProjektH3API.Application.Interfaces;
using AfsluttendeProjektH3API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AfsluttendeProjektH3API.Application.Services
{
	public class CoverService
	{
		private readonly ICoverRepository _coverRepository;
		public CoverService(ICoverRepository coverRepository)
		{
			_coverRepository = coverRepository;
		}
		public Task<Cover?> GetAsync(int id) => _coverRepository.GetByIdAsync(id);
		public Task<IEnumerable<Cover>> GetAllAsync(string? title, bool? digitalOnly) => _coverRepository.GetFilteredAsync(title, digitalOnly);
		public Task AddAsync(Cover cover) => _coverRepository.AddAsync(cover);
		public Task UpdateAsync(Cover cover) => _coverRepository.UpdateAsync(cover);
		public Task DeleteAsync(int id) => _coverRepository.DeleteAsync(id);
        public Task<Cover?> GetCoverByBookIdAsync(int id) => _coverRepository.GetCoverByBookIdAsync(id);
        public Task<IEnumerable<Cover>> GetCoversByArtistAsync(int artistId) => _coverRepository.GetCoversByArtistIdAsync(artistId);
        public Task<IEnumerable<Cover>> GetDigitalOnlyCoversAsync(bool digitalOnly) => _coverRepository.GetDigitalOnlyCoversAsync(digitalOnly);

    }
}
