using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppMedicalAssistant_DataBase.Migrations
{
    public partial class AddMedicalInstitutionIsNullFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analyses_MedicalInstitutions_MedicalInstitutionId",
                table: "Analyses");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorVisits_MedicalInstitutions_MedicalInstitutionId",
                table: "DoctorVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalExaminations_MedicalInstitutions_MedicalInstitutionId",
                table: "MedicalExaminations");

            migrationBuilder.AlterColumn<int>(
                name: "MedicalInstitutionId",
                table: "MedicalExaminations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MedicalInstitutionId",
                table: "DoctorVisits",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorVisits_MedicalInstitutions_MedicalInstitutionId",
                table: "DoctorVisits",
                column: "MedicalInstitutionId",
                principalTable: "MedicalInstitutions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalExaminations_MedicalInstitutions_MedicalInstitutionId",
                table: "MedicalExaminations",
                column: "MedicalInstitutionId",
                principalTable: "MedicalInstitutions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analyses_MedicalInstitutions_MedicalInstitutionId",
                table: "Analyses");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorVisits_MedicalInstitutions_MedicalInstitutionId",
                table: "DoctorVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalExaminations_MedicalInstitutions_MedicalInstitutionId",
                table: "MedicalExaminations");

            migrationBuilder.AlterColumn<int>(
                name: "MedicalInstitutionId",
                table: "MedicalExaminations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MedicalInstitutionId",
                table: "DoctorVisits",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorVisits_MedicalInstitutions_MedicalInstitutionId",
                table: "DoctorVisits",
                column: "MedicalInstitutionId",
                principalTable: "MedicalInstitutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalExaminations_MedicalInstitutions_MedicalInstitutionId",
                table: "MedicalExaminations",
                column: "MedicalInstitutionId",
                principalTable: "MedicalInstitutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
