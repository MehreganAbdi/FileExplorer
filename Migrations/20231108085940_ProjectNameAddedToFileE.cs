using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileExplorer.Migrations
{
    /// <inheritdoc />
    public partial class ProjectNameAddedToFileE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "Files",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "Files");
        }
    }
}
