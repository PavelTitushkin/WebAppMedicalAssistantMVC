using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppMedicalAssistantDataBase.Migrations
{
    /// <inheritdoc />
    public partial class ChangePhysicalTherapy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndPhysicalTherapy",
                table: "PhysicalTherapy");

            migrationBuilder.RenameColumn(
                name: "StartPhysicalTherapy",
                table: "PhysicalTherapy",
                newName: "DatePhysicalTherapy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DatePhysicalTherapy",
                table: "PhysicalTherapy",
                newName: "StartPhysicalTherapy");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndPhysicalTherapy",
                table: "PhysicalTherapy",
                type: "datetime2",
                nullable: true);
        }
    }
}
