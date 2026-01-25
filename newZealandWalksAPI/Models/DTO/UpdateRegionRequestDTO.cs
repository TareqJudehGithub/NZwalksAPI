using System.ComponentModel.DataAnnotations;

namespace newZealandWalksAPI.Models.DTO
{
    public class UpdateRegionRequestDTO
    {
        [Required]
        [MinLength(length: 3, ErrorMessage = "Region Code cannot be less than {1} characters.")]
        [MaxLength(length: 3, ErrorMessage = "Region Code cannot be more than {1} characters.")]
        public string Code { get; set; }

        [Required]
        [MaxLength(length: 100, ErrorMessage = "Region Name must not exceed {1} characters.")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
