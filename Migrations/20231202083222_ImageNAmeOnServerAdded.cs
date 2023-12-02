using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileExplorer.Migrations
{
    /// <inheritdoc />
    public partial class ImageNAmeOnServerAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePathOnServer",
                table: "Files",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePathOnServer",
                table: "Files");
        }
    }
}
