using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppMedicalAssistantDataBase.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTransferredDiseaseListDoctorVisit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_TransferredDiseases_TransferredDiseaseId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_TransferredDiseaseId",
                table: "Appointments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Appointments_TransferredDiseaseId",
                table: "Appointments",
                column: "TransferredDiseaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_TransferredDiseases_TransferredDiseaseId",
                table: "Appointments",
                column: "TransferredDiseaseId",
                principalTable: "TransferredDiseases",
                principalColumn: "Id");
        }
    }
}
