using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NzWalksDBContext _dbContext;
        
        public SQLRegionRepository(NzWalksDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _dbContext.AddAsync(region);
            await _dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid Id)
        {
            var existingRegin = await _dbContext.regions.FirstOrDefaultAsync(x => x.Id == Id);            

            if (existingRegin == null)
            {
                return null;
            }

            _dbContext.regions.Remove(existingRegin);
            await _dbContext.SaveChangesAsync();
            return existingRegin;

        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbContext.regions.ToListAsync();
        }

        public async Task<Region?> GetRegionByIdAsync(Guid Id)
        {
            return await _dbContext.regions.FirstOrDefaultAsync(r => r.Id == Id);
        }

        public async Task<Region> UpdateAsync(Guid Id, Region region)
        {
            var existingRegiion = await  _dbContext.regions.FirstOrDefaultAsync(x => x.Id == Id);
            
            if (existingRegiion == null)
            {
                return null;
            }
            existingRegiion.Code = region.Code;
            existingRegiion.Name = region.Name;
            existingRegiion.RegionImageUrl = region.RegionImageUrl;

            await _dbContext.SaveChangesAsync();
            return existingRegiion;
        }
    }
}
