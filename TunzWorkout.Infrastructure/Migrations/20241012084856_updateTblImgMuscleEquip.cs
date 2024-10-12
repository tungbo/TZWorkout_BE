using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TunzWorkout.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateTblImgMuscleEquip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_ImageableTypes_ImageableTypeId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "ImageableTypes");

            migrationBuilder.DropIndex(
                name: "IX_Images_ImageableTypeId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "MuscleImageId",
                table: "Muscles");

            migrationBuilder.DropColumn(
                name: "ImageableTypeId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "EquipmentImageId",
                table: "Equipments");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Images");

            migrationBuilder.AddColumn<Guid>(
                name: "MuscleImageId",
                table: "Muscles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ImageableTypeId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EquipmentImageId",
                table: "Equipments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ImageableTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageableTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_ImageableTypeId",
                table: "Images",
                column: "ImageableTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_ImageableTypes_ImageableTypeId",
                table: "Images",
                column: "ImageableTypeId",
                principalTable: "ImageableTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
