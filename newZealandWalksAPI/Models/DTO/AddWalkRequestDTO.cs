using System.ComponentModel.DataAnnotations;

namespace newZealandWalksAPI.Models.DTO
{
    public class AddWalkRequestDTO
    {
        [Required]
        [MaxLength(length: 100, ErrorMessage = "Walk Name must not exceed {1} characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(length: 250, ErrorMessage = "Description must not exceed {1} characters")]
        public string Description { get; set; }

        [Required]
        [Range(minimum: 0.1, maximum: 100, ErrorMessage = "Walk Length should be between {1} and {2} kilometer.")]
        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        // Navigation property  One to One relations
        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }
    }
}
