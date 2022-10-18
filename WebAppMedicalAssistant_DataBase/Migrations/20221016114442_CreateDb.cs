using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppMedicalAssistant_DataBase.Migrations
{
    public partial class CreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diseases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameOfDisease = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShotDescriptionDisease = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diseases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullNameDoctor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specializacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
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
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalInstitutions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameOfMedicine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescriptionOfMedicine = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
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
                    EndDateOfSurvey = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NumberFluorography = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Result = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MedicalInstitutionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fluorographies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fluorographies_MedicalInstitutions_MedicalInstitutionId",
                        column: x => x.MedicalInstitutionId,
                        principalTable: "MedicalInstitutions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Fluorographies_Users_UserId",
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
                    TypeOfTreatment = table.Column<bool>(type: "bit", nullable: false),
                    FormOfTransferredDisease = table.Column<int>(type: "int", nullable: false),
                    DiseaseId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferredDiseases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferredDiseases_Diseases_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    MedicalInstitutionId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vaccinations_MedicalInstitutions_MedicalInstitutionId",
                        column: x => x.MedicalInstitutionId,
                        principalTable: "MedicalInstitutions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vaccinations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DoctorVisits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateVisit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PriceVisit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MedicalInstitutionId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    TransferredDiseaseId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorVisits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorVisits_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorVisits_MedicalInstitutions_MedicalInstitutionId",
                        column: x => x.MedicalInstitutionId,
                        principalTable: "MedicalInstitutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorVisits_TransferredDiseases_TransferredDiseaseId",
                        column: x => x.TransferredDiseaseId,
                        principalTable: "TransferredDiseases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DoctorVisits_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DescriptionOfDestination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransferredDiseaseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_DoctorVisits_Id",
                        column: x => x.Id,
                        principalTable: "DoctorVisits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_TransferredDiseases_TransferredDiseaseId",
                        column: x => x.TransferredDiseaseId,
                        principalTable: "TransferredDiseases",
                        principalColumn: "Id");
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
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MedicalInstitutionId = table.Column<int>(type: "int", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analyses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Analyses_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Analyses_MedicalInstitutions_MedicalInstitutionId",
                        column: x => x.MedicalInstitutionId,
                        principalTable: "MedicalInstitutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Analyses_Users_UserId",
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
                    AppointmentId = table.Column<int>(type: "int", nullable: true),
                    MedicalInstitutionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalExaminations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalExaminations_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MedicalExaminations_MedicalInstitutions_MedicalInstitutionId",
                        column: x => x.MedicalInstitutionId,
                        principalTable: "MedicalInstitutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalExaminations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    MedicalInstitutionId = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_PhysicalTherapy_MedicalInstitutions_MedicalInstitutionId",
                        column: x => x.MedicalInstitutionId,
                        principalTable: "MedicalInstitutions",
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
                    MedicineId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescribedMedications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrescribedMedications_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PrescribedMedications_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrescribedMedications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Analyses_AppointmentId",
                table: "Analyses",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Analyses_MedicalInstitutionId",
                table: "Analyses",
                column: "MedicalInstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Analyses_UserId",
                table: "Analyses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_TransferredDiseaseId",
                table: "Appointments",
                column: "TransferredDiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorVisits_DoctorId",
                table: "DoctorVisits",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorVisits_MedicalInstitutionId",
                table: "DoctorVisits",
                column: "MedicalInstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorVisits_TransferredDiseaseId",
                table: "DoctorVisits",
                column: "TransferredDiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorVisits_UserId",
                table: "DoctorVisits",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Fluorographies_MedicalInstitutionId",
                table: "Fluorographies",
                column: "MedicalInstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Fluorographies_UserId",
                table: "Fluorographies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalExaminations_AppointmentId",
                table: "MedicalExaminations",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalExaminations_MedicalInstitutionId",
                table: "MedicalExaminations",
                column: "MedicalInstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalExaminations_UserId",
                table: "MedicalExaminations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalTherapy_AppointmentId",
                table: "PhysicalTherapy",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalTherapy_MedicalInstitutionId",
                table: "PhysicalTherapy",
                column: "MedicalInstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescribedMedications_AppointmentId",
                table: "PrescribedMedications",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescribedMedications_MedicineId",
                table: "PrescribedMedications",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescribedMedications_UserId",
                table: "PrescribedMedications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferredDiseases_DiseaseId",
                table: "TransferredDiseases",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferredDiseases_UserId",
                table: "TransferredDiseases",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccinations_MedicalInstitutionId",
                table: "Vaccinations",
                column: "MedicalInstitutionId");

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
                name: "Fluorographies");

            migrationBuilder.DropTable(
                name: "MedicalExaminations");

            migrationBuilder.DropTable(
                name: "PhysicalTherapy");

            migrationBuilder.DropTable(
                name: "PrescribedMedications");

            migrationBuilder.DropTable(
                name: "Vaccinations");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "DoctorVisits");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "MedicalInstitutions");

            migrationBuilder.DropTable(
                name: "TransferredDiseases");

            migrationBuilder.DropTable(
                name: "Diseases");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
