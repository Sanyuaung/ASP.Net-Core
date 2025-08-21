using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IDifficultyRepository
    {
        Task<List<Difficulty>> GetAllAsync();
        Task<(List<Difficulty> Difficulties, int TotalCount)> GetDifficultiesAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10);
        Task<Difficulty?> GetByIdAsync(Guid id);
        Task<Difficulty> CreateAsync(Difficulty difficulty);
        Task<Difficulty?> UpdateAsync(Guid id, Difficulty difficulty);
        Task<Difficulty?> DeleteAsync(Guid id);
    }
}
