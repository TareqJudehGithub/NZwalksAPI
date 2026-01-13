using System.ComponentModel.DataAnnotations.Schema;

namespace newZealandWalksAPI.Models.Domain
{
    [Table(name: "Walks", Schema = "dbo")]
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        // Navigation property  One to One relations
        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }
    }
}
