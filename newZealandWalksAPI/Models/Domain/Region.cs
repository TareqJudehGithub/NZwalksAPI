using System.ComponentModel.DataAnnotations.Schema;

namespace newZealandWalksAPI.Models.Domain
{
    [Table(name: "Regions", Schema = "dbo")]
    public class Region
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
