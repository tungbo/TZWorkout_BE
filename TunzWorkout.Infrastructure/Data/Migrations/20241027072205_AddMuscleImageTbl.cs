using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TunzWorkout.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMuscleImageTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MuscleImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MuscleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuscleImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MuscleImages_Muscles_MuscleId",
                        column: x => x.MuscleId,
                        principalTable: "Muscles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MuscleImages_MuscleId",
                table: "MuscleImages",
                column: "MuscleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MuscleImages");
        }
    }
}
