
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using newZealandWalksAPI.Models.DTO;
using newZealandWalksAPI.Repositories;

namespace newZealandWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Fields
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        #endregion

        #region Constructors
        public AuthController(
            UserManager<IdentityUser> userManager,
            ITokenRepository tokenRepository
            )
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }
        #endregion

        #region Methods

        // POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            // Instantiate a new user object from IdentityUser
            var identityUser = new IdentityUser()
            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username
            };

            // Register the new user created
            if (
                   registerRequestDTO.Roles.Any(q => q.Equals("Reader", StringComparison.OrdinalIgnoreCase)) ||
                   registerRequestDTO.Roles.Any(q => q.Equals("Writer", StringComparison.OrdinalIgnoreCase))
                )
            {
                var identityResult = await _userManager.CreateAsync(
               user: identityUser,
               password: registerRequestDTO.Password
               );

                if (identityResult.Succeeded)
                {
                    // Add a role

                    identityResult = await _userManager.AddToRolesAsync(
                        user: identityUser,
                        roles: registerRequestDTO.Roles
                        );
                    return Ok(value: "User registration was successful!");
                }
            }
            else
            {
                return BadRequest(" Invalid Role name.");
            }
            return BadRequest(" User registration failed!");
        }

        // POST: api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var identityUser = await _userManager
                .FindByEmailAsync(loginRequestDTO.Username);

            if (identityUser != null)
            {
                var checkPasswordResult = await _userManager
                    .CheckPasswordAsync(
                    user: identityUser,
                    password: loginRequestDTO.Password
                    );

                if (checkPasswordResult)
                {
                    // Get Roles for this user
                    var roles = await _userManager.GetRolesAsync(user: identityUser);

                    if (roles != null)
                    {
                        // Create JWT token
                        var jwtToken = _tokenRepository.CreateJWTToken(
                            user: identityUser,
                            roles: roles.ToList()
                            );
                        var responseDTO = new LoginResponseDTO()
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(responseDTO);
                    }
                }
            }
            return BadRequest("Incorrect username or password!");
        }
        #endregion
    }
}
