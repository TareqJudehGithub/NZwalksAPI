using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace newZealandWalksAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdataforDifficultiesandforRegionsentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("463552a3-e133-47b1-b508-97645767a11b"), "Hard" },
                    { new Guid("64eff110-e6d7-4186-b395-d8abe8680776"), "Medium" },
                    { new Guid("886ff909-1d37-4766-8a09-4ed126c3f5b5"), "Easy" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("213785ed-8bc9-46ac-84c4-885abe3583e8"), "WGN", "Wellington", "https://i.pinimg.com/1200x/f8/8e/b4/f88eb4cc51cef11c92ad4fb0492673c7.jpg" },
                    { new Guid("35b76290-1889-4915-86cc-3534d77ed761"), "AUK", "Auckland", "https://i.pinimg.com/1200x/d0/fc/f3/d0fcf30de9d249ab8d02391fcc678cd2.jpg" },
                    { new Guid("4f91fec9-4b07-4bf6-8d81-e6a7430f19c2"), "ROT", "Rotorua", "https://i.pinimg.com/1200x/a4/45/39/a44539536298c325706db782491167c3.jpg" },
                    { new Guid("974a1b4e-939b-4dbb-b798-4a7924b947d0"), "HLZ", "Hamilton", "https://i.pinimg.com/1200x/e0/82/98/e08298d1c4f3d29b92c15265660c4b4b.jpg" },
                    { new Guid("a06e1ddb-83bf-4ffc-aac1-7b7c4bf7f822"), "NTL", "Northland", "https://i.pinimg.com/736x/24/df/0e/24df0e95d0f76a668a680ccf0a88fcdb.jpg" },
                    { new Guid("ae96acd9-49eb-480c-ab85-4a372e961227"), "QNT", "Queenstown", "https://i.pinimg.com/1200x/0a/d6/26/0ad62681527a91b9054c1a362cc64228.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("463552a3-e133-47b1-b508-97645767a11b"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("64eff110-e6d7-4186-b395-d8abe8680776"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("886ff909-1d37-4766-8a09-4ed126c3f5b5"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("213785ed-8bc9-46ac-84c4-885abe3583e8"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("35b76290-1889-4915-86cc-3534d77ed761"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("4f91fec9-4b07-4bf6-8d81-e6a7430f19c2"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("974a1b4e-939b-4dbb-b798-4a7924b947d0"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a06e1ddb-83bf-4ffc-aac1-7b7c4bf7f822"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("ae96acd9-49eb-480c-ab85-4a372e961227"));
        }
    }
}
