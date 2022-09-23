using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppMedicalAssistant_DataBase.Migrations
{
    public partial class AddFieldsToTableFluorografy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicalInstitutionId",
                table: "Fluorographies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumberFluorography",
                table: "Fluorographies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Fluorographies_MedicalInstitutionId",
                table: "Fluorographies",
                column: "MedicalInstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fluorographies_MedicalInstitutions_MedicalInstitutionId",
                table: "Fluorographies",
                column: "MedicalInstitutionId",
                principalTable: "MedicalInstitutions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fluorographies_MedicalInstitutions_MedicalInstitutionId",
                table: "Fluorographies");

            migrationBuilder.DropIndex(
                name: "IX_Fluorographies_MedicalInstitutionId",
                table: "Fluorographies");

            migrationBuilder.DropColumn(
                name: "MedicalInstitutionId",
                table: "Fluorographies");

            migrationBuilder.DropColumn(
                name: "NumberFluorography",
                table: "Fluorographies");
        }
    }
}
