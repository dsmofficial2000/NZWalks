using NZWalks.API.Models.Domain;

namespace NZWalks.API.Models.DTO
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);        
    }
}
