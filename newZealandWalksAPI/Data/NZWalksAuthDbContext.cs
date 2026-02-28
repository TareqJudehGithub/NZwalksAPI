using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace newZealandWalksAPI.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Roles Ids
            var readerRoleId = "68CE350B-8CA3-4095-93DE-C50DD05AC082";
            var writerRoleId = "EEA4FE20-0D9A-4C9E-ACD6-F5CDA2AF3A3D";

            // Creating a roles list
            var roles = new List<IdentityRole>()
            {
                // ReaderRole
                new IdentityRole()
                {
                    Id = readerRoleId ,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                // WriterRole
                new IdentityRole()
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };
            // Seeding data (inserting roles created using builder HasData method) into the database upon migration
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
