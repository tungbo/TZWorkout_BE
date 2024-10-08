using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TunzWorkout.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Muscles_Images_ImageId",
                table: "Muscles");

            migrationBuilder.DropIndex(
                name: "IX_Muscles_ImageId",
                table: "Muscles");

            migrationBuilder.AlterColumn<Guid>(
                name: "ImageId",
                table: "Muscles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Muscles_ImageId",
                table: "Muscles",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Muscles_Images_ImageId",
                table: "Muscles",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Muscles_Images_ImageId",
                table: "Muscles");

            migrationBuilder.DropIndex(
                name: "IX_Muscles_ImageId",
                table: "Muscles");

            migrationBuilder.AlterColumn<Guid>(
                name: "ImageId",
                table: "Muscles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Muscles_ImageId",
                table: "Muscles",
                column: "ImageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Muscles_Images_ImageId",
                table: "Muscles",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
