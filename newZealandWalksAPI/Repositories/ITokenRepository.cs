using Microsoft.AspNetCore.Identity;

namespace newZealandWalksAPI.Repositories
{
    public interface ITokenRepository
    {
        // Create a JWT Token interfance, that accepts a user and a role arguments.
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
