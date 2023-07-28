using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DutyUpdateed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Duties_Duties_DutyId",
                table: "Duties");

            migrationBuilder.DropIndex(
                name: "IX_Duties_DutyId",
                table: "Duties");

            migrationBuilder.DropColumn(
                name: "DutyId",
                table: "Duties");

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DutyId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModfiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctor_Duties_DutyId",
                        column: x => x.DutyId,
                        principalTable: "Duties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_DutyId",
                table: "Doctor",
                column: "DutyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.AddColumn<int>(
                name: "DutyId",
                table: "Duties",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Duties_DutyId",
                table: "Duties",
                column: "DutyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Duties_Duties_DutyId",
                table: "Duties",
                column: "DutyId",
                principalTable: "Duties",
                principalColumn: "Id");
        }
    }
}
