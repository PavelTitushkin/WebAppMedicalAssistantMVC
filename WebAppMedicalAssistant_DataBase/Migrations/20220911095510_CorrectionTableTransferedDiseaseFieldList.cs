using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppMedicalAssistant_DataBase.Migrations
{
    public partial class CorrectionTableTransferedDiseaseFieldList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferredDiseases_TransferredDiseases_TransferredDiseaseId",
                table: "TransferredDiseases");

            migrationBuilder.DropIndex(
                name: "IX_TransferredDiseases_TransferredDiseaseId",
                table: "TransferredDiseases");

            migrationBuilder.DropColumn(
                name: "TransferredDiseaseId",
                table: "TransferredDiseases");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransferredDiseaseId",
                table: "TransferredDiseases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransferredDiseases_TransferredDiseaseId",
                table: "TransferredDiseases",
                column: "TransferredDiseaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferredDiseases_TransferredDiseases_TransferredDiseaseId",
                table: "TransferredDiseases",
                column: "TransferredDiseaseId",
                principalTable: "TransferredDiseases",
                principalColumn: "Id");
        }
    }
}
