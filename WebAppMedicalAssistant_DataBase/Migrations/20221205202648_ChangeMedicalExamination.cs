using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppMedicalAssistantDataBase.Migrations
{
    /// <inheritdoc />
    public partial class ChangeMedicalExamination : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScanOfMedicalExamination",
                table: "MedicalExaminations");

            migrationBuilder.CreateTable(
                name: "ScanOfMedicalExamination",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScanData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    MedicalExaminationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScanOfMedicalExamination", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScanOfMedicalExamination_MedicalExaminations_MedicalExaminationId",
                        column: x => x.MedicalExaminationId,
                        principalTable: "MedicalExaminations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScanOfMedicalExamination_MedicalExaminationId",
                table: "ScanOfMedicalExamination",
                column: "MedicalExaminationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScanOfMedicalExamination");

            migrationBuilder.AddColumn<byte[]>(
                name: "ScanOfMedicalExamination",
                table: "MedicalExaminations",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
