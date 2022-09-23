using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppMedicalAssistant_DataBase.Migrations
{
    public partial class EditingTableTransferedDisease : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormOfTransferredDisease",
                table: "TransferredDiseases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "TypeOfTreatment",
                table: "TransferredDiseases",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TransferredDiseaseId",
                table: "PrescribedMedications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransferredDiseaseId",
                table: "DoctorVisits",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PhysicalTherapy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameOfPhysicalTherapy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartPhysicalTherapy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndPhysicalTherapy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: true),
                    TransferredDiseaseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalTherapy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhysicalTherapy_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PhysicalTherapy_TransferredDiseases_TransferredDiseaseId",
                        column: x => x.TransferredDiseaseId,
                        principalTable: "TransferredDiseases",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrescribedMedications_TransferredDiseaseId",
                table: "PrescribedMedications",
                column: "TransferredDiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorVisits_TransferredDiseaseId",
                table: "DoctorVisits",
                column: "TransferredDiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalTherapy_AppointmentId",
                table: "PhysicalTherapy",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalTherapy_TransferredDiseaseId",
                table: "PhysicalTherapy",
                column: "TransferredDiseaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorVisits_TransferredDiseases_TransferredDiseaseId",
                table: "DoctorVisits",
                column: "TransferredDiseaseId",
                principalTable: "TransferredDiseases",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescribedMedications_TransferredDiseases_TransferredDiseaseId",
                table: "PrescribedMedications",
                column: "TransferredDiseaseId",
                principalTable: "TransferredDiseases",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorVisits_TransferredDiseases_TransferredDiseaseId",
                table: "DoctorVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_PrescribedMedications_TransferredDiseases_TransferredDiseaseId",
                table: "PrescribedMedications");

            migrationBuilder.DropTable(
                name: "PhysicalTherapy");

            migrationBuilder.DropIndex(
                name: "IX_PrescribedMedications_TransferredDiseaseId",
                table: "PrescribedMedications");

            migrationBuilder.DropIndex(
                name: "IX_DoctorVisits_TransferredDiseaseId",
                table: "DoctorVisits");

            migrationBuilder.DropColumn(
                name: "FormOfTransferredDisease",
                table: "TransferredDiseases");

            migrationBuilder.DropColumn(
                name: "TypeOfTreatment",
                table: "TransferredDiseases");

            migrationBuilder.DropColumn(
                name: "TransferredDiseaseId",
                table: "PrescribedMedications");

            migrationBuilder.DropColumn(
                name: "TransferredDiseaseId",
                table: "DoctorVisits");
        }
    }
}
