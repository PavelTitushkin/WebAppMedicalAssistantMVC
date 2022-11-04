using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppMedicalAssistant_DataBase.Migrations
{
    public partial class ChangeMedicine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShortDescriptionOfMedicine",
                table: "Medicines",
                newName: "WayOfIntroduction");

            migrationBuilder.AddColumn<string>(
                name: "DosageForm",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LinkToInstructions",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReleaseForm",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DosageForm",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "LinkToInstructions",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "ReleaseForm",
                table: "Medicines");

            migrationBuilder.RenameColumn(
                name: "WayOfIntroduction",
                table: "Medicines",
                newName: "ShortDescriptionOfMedicine");
        }
    }
}
