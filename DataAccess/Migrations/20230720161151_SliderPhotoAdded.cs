using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SliderPhotoAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Sliders",
                newName: "Subtitle");

            migrationBuilder.AddColumn<string>(
                name: "Photoname",
                table: "Sliders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photoname",
                table: "Sliders");

            migrationBuilder.RenameColumn(
                name: "Subtitle",
                table: "Sliders",
                newName: "Name");
        }
    }
}
