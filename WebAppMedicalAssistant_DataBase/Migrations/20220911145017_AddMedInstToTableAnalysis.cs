using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppMedicalAssistant_DataBase.Migrations
{
    public partial class AddMedInstToTableAnalysis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicalInstitutionId",
                table: "Analyses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Analyses_MedicalInstitutionId",
                table: "Analyses",
                column: "MedicalInstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Analyses_MedicalInstitutions_MedicalInstitutionId",
                table: "Analyses",
                column: "MedicalInstitutionId",
                principalTable: "MedicalInstitutions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analyses_MedicalInstitutions_MedicalInstitutionId",
                table: "Analyses");

            migrationBuilder.DropIndex(
                name: "IX_Analyses_MedicalInstitutionId",
                table: "Analyses");

            migrationBuilder.DropColumn(
                name: "MedicalInstitutionId",
                table: "Analyses");
        }
    }
}
