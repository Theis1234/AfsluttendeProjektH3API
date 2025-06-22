using AfsluttendeProjektH3API.Application.Interfaces;
using AfsluttendeProjektH3API.Domain.Entities;

namespace AfsluttendeProjektH3API.Application;

public class AwardService : IAwardService
{
    private readonly IGenericRepository<Award> _awardRepository;

    public AwardService(IGenericRepository<Award> awardRepository)
    {
        _awardRepository = awardRepository;
    }

    public async Task<IEnumerable<Award>> GetAllAwardsAsync() => await _awardRepository.GetAllAsync();
    public async Task<IEnumerable<Award>> GetAwardsByArtistIdAsync(int artistId) => await _awardRepository.FindAsync(a => a.ArtistId == artistId);

    public Task<Award?> GetAsync(int id) => _awardRepository.GetByIdAsync(id);
    public Task AddAsync(Award award) => _awardRepository.AddAsync(award);
    public Task UpdateAsync(Award award) => _awardRepository.UpdateAsync(award);
    public Task DeleteAsync(int id) => _awardRepository.DeleteAsync(id);
}
