using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppMedicalAssistantDataBase.Migrations
{
    /// <inheritdoc />
    public partial class ChangeMedicalExaminationAddIBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScanOfMedicalExamination_MedicalExaminations_MedicalExaminationId",
                table: "ScanOfMedicalExamination");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScanOfMedicalExamination",
                table: "ScanOfMedicalExamination");

            migrationBuilder.RenameTable(
                name: "ScanOfMedicalExamination",
                newName: "ScanOfMedicalExaminations");

            migrationBuilder.RenameIndex(
                name: "IX_ScanOfMedicalExamination_MedicalExaminationId",
                table: "ScanOfMedicalExaminations",
                newName: "IX_ScanOfMedicalExaminations_MedicalExaminationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScanOfMedicalExaminations",
                table: "ScanOfMedicalExaminations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScanOfMedicalExaminations_MedicalExaminations_MedicalExaminationId",
                table: "ScanOfMedicalExaminations",
                column: "MedicalExaminationId",
                principalTable: "MedicalExaminations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScanOfMedicalExaminations_MedicalExaminations_MedicalExaminationId",
                table: "ScanOfMedicalExaminations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScanOfMedicalExaminations",
                table: "ScanOfMedicalExaminations");

            migrationBuilder.RenameTable(
                name: "ScanOfMedicalExaminations",
                newName: "ScanOfMedicalExamination");

            migrationBuilder.RenameIndex(
                name: "IX_ScanOfMedicalExaminations_MedicalExaminationId",
                table: "ScanOfMedicalExamination",
                newName: "IX_ScanOfMedicalExamination_MedicalExaminationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScanOfMedicalExamination",
                table: "ScanOfMedicalExamination",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScanOfMedicalExamination_MedicalExaminations_MedicalExaminationId",
                table: "ScanOfMedicalExamination",
                column: "MedicalExaminationId",
                principalTable: "MedicalExaminations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
