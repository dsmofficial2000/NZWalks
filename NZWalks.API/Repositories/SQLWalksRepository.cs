using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalksRepository : IWalksRepository
    {
        private readonly NzWalksDBContext dbcontext;        
        public SQLWalksRepository(NzWalksDBContext dBContext)        
        {
            this.dbcontext = dBContext;            
        }        
        
        public async Task<Walk> CreateAsync(Walk walk)
        {   
            await dbcontext.AddAsync(walk);
            await dbcontext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {            
            var delteExistingWalk = await dbcontext.walks.FirstOrDefaultAsync(x => x.Id == id);
            if (delteExistingWalk == null)
            {
                return null;
            }
            dbcontext.walks.Remove(delteExistingWalk);
            await dbcontext.SaveChangesAsync();
            return delteExistingWalk;
        }

        public async Task<List<Walk>> GetallAsync()
        {
            return await dbcontext.walks.ToListAsync();            
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbcontext.walks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbcontext.walks.FirstOrDefaultAsync(x => x.Id == id);            
            
            if (existingWalk == null)
            {
                return null;
            }
            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;
            
            await dbcontext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
