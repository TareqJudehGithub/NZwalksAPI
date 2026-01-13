using System.ComponentModel.DataAnnotations.Schema;

namespace newZealandWalksAPI.Models.Domain
{
    [Table(name: "Difficulties", Schema = "dbo")]
    public class Difficulty
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
