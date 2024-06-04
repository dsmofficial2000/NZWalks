using Microsoft.EntityFrameworkCore;
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
        
        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbContext.regions.ToListAsync();
        }
    }
}
