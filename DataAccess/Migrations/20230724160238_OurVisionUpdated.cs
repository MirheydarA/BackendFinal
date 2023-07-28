using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class OurVisionUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OurVisions_OurVisionComponents_OurVisionComponentId",
                table: "OurVisions");

            migrationBuilder.DropIndex(
                name: "IX_OurVisions_OurVisionComponentId",
                table: "OurVisions");

            migrationBuilder.DropColumn(
                name: "OurVisionComponentId",
                table: "OurVisions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OurVisionComponentId",
                table: "OurVisions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OurVisions_OurVisionComponentId",
                table: "OurVisions",
                column: "OurVisionComponentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OurVisions_OurVisionComponents_OurVisionComponentId",
                table: "OurVisions",
                column: "OurVisionComponentId",
                principalTable: "OurVisionComponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
