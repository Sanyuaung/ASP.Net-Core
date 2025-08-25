using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        #region Constructor
        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion

        #region GetAllAsync
        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }
        #endregion

        #region GetRegionsAsync
        public async Task<(List<Region> Regions, int TotalCount)> GetRegionsAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var query = dbContext.Regions.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(x => x.Name.Contains(filterQuery));
                }
                else if (filterOn.Equals("Code", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(x => x.Code.Contains(filterQuery));
                }
            }

            var totalCount = await query.CountAsync();

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    query = isAscending ? query.OrderBy(x => x.Name) : query.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Code", StringComparison.OrdinalIgnoreCase))
                {
                    query = isAscending ? query.OrderBy(x => x.Code) : query.OrderByDescending(x => x.Code);
                }
            }
            else
            {
                // Ensure deterministic ordering for pagination (avoids EF warning)
                query = query.OrderBy(x => x.Id);
            }

            var regions = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (regions, totalCount);
        }
        #endregion

        #region GetByIdAsync
        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }
        #endregion

        #region CreateAsync
        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }
        #endregion

        #region UpdateAsync
        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }
        #endregion

        #region DeleteAsync
        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            dbContext.Regions.Remove(existingRegion);
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }
        #endregion
    }
}
