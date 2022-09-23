using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppMedicalAssistant_DataBase.Migrations
{
    public partial class EditingDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_MedicalExaminations_MedicalExaminationId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorVisits_TransferredDiseases_TransferredDiseaseId",
                table: "DoctorVisits");

            migrationBuilder.DropIndex(
                name: "IX_DoctorVisits_TransferredDiseaseId",
                table: "DoctorVisits");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_MedicalExaminationId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "TransferredDiseaseId",
                table: "DoctorVisits");

            migrationBuilder.DropColumn(
                name: "MedicalExaminationId",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "DoctorVisitId",
                table: "TransferredDiseases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "PrescribedMedications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "MedicalExaminations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransferredDiseases_DoctorVisitId",
                table: "TransferredDiseases",
                column: "DoctorVisitId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescribedMedications_AppointmentId",
                table: "PrescribedMedications",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalExaminations_AppointmentId",
                table: "MedicalExaminations",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalExaminations_Appointments_AppointmentId",
                table: "MedicalExaminations",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescribedMedications_Appointments_AppointmentId",
                table: "PrescribedMedications",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferredDiseases_DoctorVisits_DoctorVisitId",
                table: "TransferredDiseases",
                column: "DoctorVisitId",
                principalTable: "DoctorVisits",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalExaminations_Appointments_AppointmentId",
                table: "MedicalExaminations");

            migrationBuilder.DropForeignKey(
                name: "FK_PrescribedMedications_Appointments_AppointmentId",
                table: "PrescribedMedications");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferredDiseases_DoctorVisits_DoctorVisitId",
                table: "TransferredDiseases");

            migrationBuilder.DropIndex(
                name: "IX_TransferredDiseases_DoctorVisitId",
                table: "TransferredDiseases");

            migrationBuilder.DropIndex(
                name: "IX_PrescribedMedications_AppointmentId",
                table: "PrescribedMedications");

            migrationBuilder.DropIndex(
                name: "IX_MedicalExaminations_AppointmentId",
                table: "MedicalExaminations");

            migrationBuilder.DropColumn(
                name: "DoctorVisitId",
                table: "TransferredDiseases");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "PrescribedMedications");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "MedicalExaminations");

            migrationBuilder.AddColumn<int>(
                name: "TransferredDiseaseId",
                table: "DoctorVisits",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicalExaminationId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorVisits_TransferredDiseaseId",
                table: "DoctorVisits",
                column: "TransferredDiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_MedicalExaminationId",
                table: "Appointments",
                column: "MedicalExaminationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_MedicalExaminations_MedicalExaminationId",
                table: "Appointments",
                column: "MedicalExaminationId",
                principalTable: "MedicalExaminations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorVisits_TransferredDiseases_TransferredDiseaseId",
                table: "DoctorVisits",
                column: "TransferredDiseaseId",
                principalTable: "TransferredDiseases",
                principalColumn: "Id");
        }
    }
}
