using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLDifficultyRepository : IDifficultyRepository
    {
        private readonly NZWalksDbContext dbContext;
        #region Constructor
        public SQLDifficultyRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion

        #region GetAllAsync
        public async Task<List<Difficulty>> GetAllAsync()
        {
            return await dbContext.Difficulties.ToListAsync();
        }
        #endregion

        #region GetDifficultiesAsync
        public async Task<(List<Difficulty> Difficulties, int TotalCount)> GetDifficultiesAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var query = dbContext.Difficulties.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(x => x.Name.Contains(filterQuery));
                }
            }

            var totalCount = await query.CountAsync();

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    query = isAscending ? query.OrderBy(x => x.Name) : query.OrderByDescending(x => x.Name);
                }
            }

            var difficulties = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (difficulties, totalCount);
        }
        #endregion

        #region GetByIdAsync
        public async Task<Difficulty?> GetByIdAsync(Guid id)
        {
            return await dbContext.Difficulties.FirstOrDefaultAsync(x => x.Id == id);
        }
        #endregion

        #region CreateAsync
        public async Task<Difficulty> CreateAsync(Difficulty difficulty)
        {
            await dbContext.Difficulties.AddAsync(difficulty);
            await dbContext.SaveChangesAsync();
            return difficulty;
        }
        #endregion

        #region UpdateAsync
        public async Task<Difficulty?> UpdateAsync(Guid id,Difficulty difficulty)
        {
            var exitingDifficulty = await dbContext.Difficulties.FirstOrDefaultAsync(x => x.Id == id);
            if (exitingDifficulty == null)
            {
                return null;
            }
            exitingDifficulty.Name = difficulty.Name;
            await dbContext.SaveChangesAsync();
            return exitingDifficulty;
        }
        #endregion

        #region DeleteAsync
        public async Task<Difficulty?> DeleteAsync(Guid id)
        {
            var existingDifficulty = await dbContext.Difficulties.FirstOrDefaultAsync(x => x.Id == id);
            if(existingDifficulty == null)
            {
                return null;
            }
            dbContext.Difficulties.Remove(existingDifficulty);
            await dbContext.SaveChangesAsync();
            return existingDifficulty;
        }
        #endregion
    }
}
