using AfsluttendeProjektH3API.Application.Interfaces;
using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application.Services
{
	public class CoverService
	{
		private readonly ICoverRepository _coverService;
		public CoverService(ICoverRepository coverRepository)
		{
			_coverService = coverRepository;
		}
		public Task<Cover?> GetAsync(int id) => _coverService.GetByIdAsync(id);
		public Task<IEnumerable<Cover>> GetAllAsync() => _coverService.GetAllAsync();
		public Task AddAsync(Cover cover) => _coverService.AddAsync(cover);
		public Task UpdateAsync(Cover cover) => _coverService.UpdateAsync(cover);
		public Task DeleteAsync(int id) => _coverService.DeleteAsync(id);
	}
}
