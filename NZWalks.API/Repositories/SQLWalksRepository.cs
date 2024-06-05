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
    }
}
