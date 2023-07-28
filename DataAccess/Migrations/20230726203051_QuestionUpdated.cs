using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class QuestionUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_FAQCategories_FAQCategoryId",
                table: "Questions");

            migrationBuilder.AlterColumn<int>(
                name: "FAQCategoryId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_FAQCategories_FAQCategoryId",
                table: "Questions",
                column: "FAQCategoryId",
                principalTable: "FAQCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_FAQCategories_FAQCategoryId",
                table: "Questions");

            migrationBuilder.AlterColumn<int>(
                name: "FAQCategoryId",
                table: "Questions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_FAQCategories_FAQCategoryId",
                table: "Questions",
                column: "FAQCategoryId",
                principalTable: "FAQCategories",
                principalColumn: "Id");
        }
    }
}
