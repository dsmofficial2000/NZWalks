using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Models.DTO
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
