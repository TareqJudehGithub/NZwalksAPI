using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace newZealandWalksAPI.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedspellingmistakeinoneofImagetablecolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileExtention",
                table: "Images",
                newName: "FileExtension");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileExtension",
                table: "Images",
                newName: "FileExtention");
        }
    }
}
