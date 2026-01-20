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

        // Overriding OnModelCreating method, in order to seed initial data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding data for Difficulties
            // Easy, Medium, Hard
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("886ff909-1d37-4766-8a09-4ed126c3f5b5"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("64EFF110-E6D7-4186-B395-D8ABE8680776"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("463552A3-E133-47B1-B508-97645767A11B"),
                    Name = "Hard"
                }
            };

            // Seed difficulties list to the database table/entity Difficulty
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // Seeding data for Regions
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("35b76290-1889-4915-86cc-3534d77ed761"),
                    Code = "AUK",
                    Name = "Auckland",
                    RegionImageUrl = "https://i.pinimg.com/1200x/d0/fc/f3/d0fcf30de9d249ab8d02391fcc678cd2.jpg"
                },
                 new Region()
                {
                    Id = Guid.Parse("a06e1ddb-83bf-4ffc-aac1-7b7c4bf7f822"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = "https://i.pinimg.com/736x/24/df/0e/24df0e95d0f76a668a680ccf0a88fcdb.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("ae96acd9-49eb-480c-ab85-4a372e961227"),
                    Name = "Queenstown",
                    Code = "QNT",
                    RegionImageUrl = "https://i.pinimg.com/1200x/0a/d6/26/0ad62681527a91b9054c1a362cc64228.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("213785ed-8bc9-46ac-84c4-885abe3583e8"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = "https://i.pinimg.com/1200x/f8/8e/b4/f88eb4cc51cef11c92ad4fb0492673c7.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("974a1b4e-939b-4dbb-b798-4a7924b947d0"),
                    Name = "Hamilton",
                    Code = "HLZ",
                    RegionImageUrl = "https://i.pinimg.com/1200x/e0/82/98/e08298d1c4f3d29b92c15265660c4b4b.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("4f91fec9-4b07-4bf6-8d81-e6a7430f19c2"),
                    Name = "Rotorua",
                    Code = "ROT",
                    RegionImageUrl = "https://i.pinimg.com/1200x/a4/45/39/a44539536298c325706db782491167c3.jpg"
                }
            };

            // Insertion into DB
            modelBuilder.Entity<Region>().HasData(regions);

        }


    }
}

/* 
 * "code": "AUK",
  "name": "Auckland",
  "regionImageUrl": "https://i.pinimg.com/1200x/d0/fc/f3/d0fcf30de9d249ab8d02391fcc678cd2.jpg"
 */