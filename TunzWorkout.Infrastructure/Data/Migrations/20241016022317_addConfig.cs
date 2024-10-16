using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TunzWorkout.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Videos_VideoId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_VideoId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "VideoId",
                table: "Exercises");

            migrationBuilder.AddColumn<Guid>(
                name: "ExerciseId",
                table: "Videos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Videos_ExerciseId",
                table: "Videos",
                column: "ExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Exercises_ExerciseId",
                table: "Videos",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Exercises_ExerciseId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_ExerciseId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "ExerciseId",
                table: "Videos");

            migrationBuilder.AddColumn<Guid>(
                name: "VideoId",
                table: "Exercises",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_VideoId",
                table: "Exercises",
                column: "VideoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Videos_VideoId",
                table: "Exercises",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
