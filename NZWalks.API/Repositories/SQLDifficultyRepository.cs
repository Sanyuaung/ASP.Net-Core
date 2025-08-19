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
