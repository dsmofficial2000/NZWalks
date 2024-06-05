using NZWalks.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalksRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetallAsync();        
        Task<Walk?> GetByIdAsync(Guid id);
        //Task<Walk?> UpdateAsync(Guid id, Walk walk);
        //Task<Walk?> DeleteAsync(Guid id);
    }
}
