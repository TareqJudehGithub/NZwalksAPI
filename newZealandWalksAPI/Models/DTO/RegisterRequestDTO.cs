using System.ComponentModel.DataAnnotations;

namespace newZealandWalksAPI.Models.DTO
{
    public class RegisterRequestDTO
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare(otherProperty: "Password", ErrorMessage = "{1} and Confirm Password must match!")]
        public string ConfirmPassword { get; set; }

        // Roles
        [Required]
        public string[] Roles { get; set; }
    }
}
