using Microsoft.EntityFrameworkCore;
using newZealandWalksAPI.Models.Domain;

namespace newZealandWalksAPI.Data
{
    public class NZWalksDbContext : DbContext
    {
        #region Constructor
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
        #endregion

        #region Entities
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        #endregion
    }
}
