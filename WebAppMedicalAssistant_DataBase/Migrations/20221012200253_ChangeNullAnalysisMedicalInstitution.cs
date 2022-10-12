using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppMedicalAssistant_DataBase.Migrations
{
    public partial class ChangeNullAnalysisMedicalInstitution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analyses_MedicalInstitutions_MedicalInstitutionId",
                table: "Analyses");

            migrationBuilder.AlterColumn<int>(
                name: "MedicalInstitutionId",
                table: "Analyses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "MedicalInstitutionId",
                table: "Analyses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Analyses_MedicalInstitutions_MedicalInstitutionId",
                table: "Analyses",
                column: "MedicalInstitutionId",
                principalTable: "MedicalInstitutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
