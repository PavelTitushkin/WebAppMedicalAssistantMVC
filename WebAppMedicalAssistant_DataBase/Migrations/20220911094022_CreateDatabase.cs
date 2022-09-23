using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppMedicalAssistant_DataBase.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Analyses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameOfAnalysis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfAnalysis = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScanOfAnalysisDocument = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analyses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Analyses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorVisits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateVisit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PriceVisit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorVisits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorVisits_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fluorographies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataOfExamination = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateOfSurvey = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fluorographies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fluorographies_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalExaminations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameOfMedicalExamination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfMedicalExamination = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PriceOfMedicalExamination = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ScanOfMedicalExamination = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalExaminations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalExaminations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrescribedMedications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDateOfMedication = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateOfMedication = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicineDose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicinePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescribedMedications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrescribedMedications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferredDiseases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfDisease = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfRecovery = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TransferredDiseaseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferredDiseases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferredDiseases_TransferredDiseases_TransferredDiseaseId",
                        column: x => x.TransferredDiseaseId,
                        principalTable: "TransferredDiseases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransferredDiseases_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vaccinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationOfVaccine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameOfVaccine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VacineDose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VacineSeria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfVaccination = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VaccinationExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vaccinations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastNameDoctor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstNameDoctor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatronymicDoctor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specializacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: true),
                    DoctorVisitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_DoctorVisits_DoctorVisitId",
                        column: x => x.DoctorVisitId,
                        principalTable: "DoctorVisits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionOfDestination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicalExaminationId = table.Column<int>(type: "int", nullable: true),
                    DoctorVisitId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_DoctorVisits_DoctorVisitId",
                        column: x => x.DoctorVisitId,
                        principalTable: "DoctorVisits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointments_MedicalExaminations_MedicalExaminationId",
                        column: x => x.MedicalExaminationId,
                        principalTable: "MedicalExaminations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameOfMedicine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescriptionOfMedicine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrescribedMedicationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicines_PrescribedMedications_PrescribedMedicationId",
                        column: x => x.PrescribedMedicationId,
                        principalTable: "PrescribedMedications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diseases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameOfDisease = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShotDescriptionDisease = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransferredDiseaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diseases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diseases_TransferredDiseases_TransferredDiseaseId",
                        column: x => x.TransferredDiseaseId,
                        principalTable: "TransferredDiseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalInstitutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameMedicalInstitution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperatingMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorVisitId = table.Column<int>(type: "int", nullable: true),
                    VaccinationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalInstitutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalInstitutions_DoctorVisits_DoctorVisitId",
                        column: x => x.DoctorVisitId,
                        principalTable: "DoctorVisits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MedicalInstitutions_Vaccinations_VaccinationId",
                        column: x => x.VaccinationId,
                        principalTable: "Vaccinations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Analyses_UserId",
                table: "Analyses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorVisitId",
                table: "Appointments",
                column: "DoctorVisitId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_MedicalExaminationId",
                table: "Appointments",
                column: "MedicalExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_TransferredDiseaseId",
                table: "Diseases",
                column: "TransferredDiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DoctorVisitId",
                table: "Doctors",
                column: "DoctorVisitId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorVisits_UserId",
                table: "DoctorVisits",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Fluorographies_UserId",
                table: "Fluorographies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalExaminations_UserId",
                table: "MedicalExaminations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalInstitutions_DoctorVisitId",
                table: "MedicalInstitutions",
                column: "DoctorVisitId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalInstitutions_VaccinationId",
                table: "MedicalInstitutions",
                column: "VaccinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_PrescribedMedicationId",
                table: "Medicines",
                column: "PrescribedMedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescribedMedications_UserId",
                table: "PrescribedMedications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferredDiseases_TransferredDiseaseId",
                table: "TransferredDiseases",
                column: "TransferredDiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferredDiseases_UserId",
                table: "TransferredDiseases",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccinations_UserId",
                table: "Vaccinations",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Analyses");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Diseases");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Fluorographies");

            migrationBuilder.DropTable(
                name: "MedicalInstitutions");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "MedicalExaminations");

            migrationBuilder.DropTable(
                name: "TransferredDiseases");

            migrationBuilder.DropTable(
                name: "DoctorVisits");

            migrationBuilder.DropTable(
                name: "Vaccinations");

            migrationBuilder.DropTable(
                name: "PrescribedMedications");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
