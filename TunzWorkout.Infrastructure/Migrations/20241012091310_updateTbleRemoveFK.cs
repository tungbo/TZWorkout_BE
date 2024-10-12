using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TunzWorkout.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateTbleRemoveFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Equipments_EquipmentId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Muscles_MuscleId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_EquipmentId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_MuscleId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "MuscleId",
                table: "Images");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EquipmentId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MuscleId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Images_EquipmentId",
                table: "Images",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_MuscleId",
                table: "Images",
                column: "MuscleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Equipments_EquipmentId",
                table: "Images",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Muscles_MuscleId",
                table: "Images",
                column: "MuscleId",
                principalTable: "Muscles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
