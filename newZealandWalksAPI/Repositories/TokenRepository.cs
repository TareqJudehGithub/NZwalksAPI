using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;   // for using Claim
using System.Text;

namespace newZealandWalksAPI.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _configuration;

        public TokenRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            // Create claims
            var claims = new List<Claim>();
            claims.Add(new Claim(type: ClaimTypes.Email, value: user.Email));

            // Roles
            foreach (var role in roles)
            {
                claims.Add(new Claim(type: ClaimTypes.Role, value: role));
            }

            // Accesses a key (token key) 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[key: "Jwt:Key"]));

            var credentials = new SigningCredentials(key: key, algorithm: SecurityAlgorithms.HmacSha256);

            // Create the JWT token
            var token = new JwtSecurityToken(
                issuer: _configuration[key: "Jwt:Issuer"],
                audience: _configuration[key: "Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),   // expires after 15mins
                signingCredentials: credentials
                );

            // Instantiate a new JWT token from JwtSecurityTokenHandler using WriteToken() method.
            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
