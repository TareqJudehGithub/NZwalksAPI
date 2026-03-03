
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using newZealandWalksAPI.Models.DTO;

namespace newZealandWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Fields
        private readonly UserManager<IdentityUser> _userManager;
        #endregion

        #region Constructors
        public AuthController(
            UserManager<IdentityUser> userManager

            )
        {
            _userManager = userManager;
        }
        #endregion

        #region Methods

        // POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser()
            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username
            };
            // Register the new user created
            var identityResult = await _userManager.CreateAsync(
                user: identityUser,
                password: registerRequestDTO.Password
                );

            if (identityResult.Succeeded)
            {
                // Add a role
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(
                        user: identityUser,
                        roles: registerRequestDTO.Roles
                        );
                    return Ok(value: "User registration was successful!");
                }
            }
            return BadRequest(" User registration failed!");
        }

        // POST: api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var identityUser = await _userManager.FindByEmailAsync(loginRequestDTO.Username);

            if (identityUser != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(
                    user: identityUser,
                    password: loginRequestDTO.Password);

                if (checkPasswordResult)
                {
                    return Ok(value: $"Welcome, {identityUser}!");
                }
            }
            return BadRequest("Incorrect username or password!");
        }

        #endregion
    }
}
