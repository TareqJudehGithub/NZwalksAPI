namespace newZealandWalksAPI.Models.DTO
{
    public class WalkDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        // We already have both Region an Difficulty ids in the navigation properties below
        //public Guid DifficultyId { get; set; }
        //public Guid RegionId { get; set; }

        // Navigation property  One to One relations
        public RegionDTO Region { get; set; }
        public DifficultyDTO Difficulty { get; set; }
    }
}
