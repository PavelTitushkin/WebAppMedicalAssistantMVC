﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAppMedicalAssistant_DataBase;

#nullable disable

namespace WebAppMedicalAssistant_DataBase.Migrations
{
    [DbContext(typeof(MedicalAssistantContext))]
    partial class MedicalAssistantContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.Analysis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfAnalysis")
                        .HasColumnType("datetime2");

                    b.Property<int>("MedicalInstitutionId")
                        .HasColumnType("int");

                    b.Property<string>("NameOfAnalysis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ScanOfAnalysisDocument")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId");

                    b.HasIndex("MedicalInstitutionId");

                    b.HasIndex("UserId");

                    b.ToTable("Analyses");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("DescriptionOfDestination")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TransferredDiseaseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransferredDiseaseId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.Disease", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("NameOfDisease")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShotDescriptionDisease")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Diseases");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FullNameDoctor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("Rating")
                        .HasColumnType("real");

                    b.Property<string>("Specializacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.DoctorVisit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateVisit")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int>("MedicalInstitutionId")
                        .HasColumnType("int");

                    b.Property<decimal?>("PriceVisit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("TransferredDiseaseId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("MedicalInstitutionId");

                    b.HasIndex("TransferredDiseaseId");

                    b.HasIndex("UserId");

                    b.ToTable("DoctorVisits");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.Fluorography", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DataOfExamination")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndDateOfSurvey")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MedicalInstitutionId")
                        .HasColumnType("int");

                    b.Property<string>("NumberFluorography")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Result")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MedicalInstitutionId");

                    b.HasIndex("UserId");

                    b.ToTable("Fluorographies");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.MedicalExamination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfMedicalExamination")
                        .HasColumnType("datetime2");

                    b.Property<int>("MedicalInstitutionId")
                        .HasColumnType("int");

                    b.Property<string>("NameOfMedicalExamination")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("PriceOfMedicalExamination")
                        .HasColumnType("decimal(18,2)");

                    b.Property<byte[]>("ScanOfMedicalExamination")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId");

                    b.HasIndex("MedicalInstitutionId");

                    b.HasIndex("UserId");

                    b.ToTable("MedicalExaminations");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.MedicalInstitution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contact")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameMedicalInstitution")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OperatingMode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MedicalInstitutions");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.Medicine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("NameOfMedicine")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortDescriptionOfMedicine")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.PhysicalTherapy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndPhysicalTherapy")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MedicalInstitutionId")
                        .HasColumnType("int");

                    b.Property<string>("NameOfPhysicalTherapy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartPhysicalTherapy")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId");

                    b.HasIndex("MedicalInstitutionId");

                    b.HasIndex("UserId");

                    b.ToTable("PhysicalTherapy");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.PrescribedMedication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDateOfMedication")
                        .HasColumnType("datetime2");

                    b.Property<string>("MedicineDose")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MedicineId")
                        .HasColumnType("int");

                    b.Property<decimal>("MedicinePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDateOfMedication")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId");

                    b.HasIndex("MedicineId");

                    b.HasIndex("UserId");

                    b.ToTable("PrescribedMedications");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.TransferredDisease", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateOfDisease")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfRecovery")
                        .HasColumnType("datetime2");

                    b.Property<int>("DiseaseId")
                        .HasColumnType("int");

                    b.Property<int>("FormOfTransferredDisease")
                        .HasColumnType("int");

                    b.Property<bool>("TypeOfTreatment")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DiseaseId");

                    b.HasIndex("UserId");

                    b.ToTable("TransferredDiseases");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.Vaccination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ApplicationOfVaccine")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfVaccination")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MedicalInstitutionId")
                        .HasColumnType("int");

                    b.Property<string>("NameOfVaccine")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("VaccinationExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("VacineDose")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VacineSeria")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MedicalInstitutionId");

                    b.HasIndex("UserId");

                    b.ToTable("Vaccinations");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.Analysis", b =>
                {
                    b.HasOne("WebAppMedicalAssistant_DataBase.Entities.Appointment", "Appointment")
                        .WithMany("Analysis")
                        .HasForeignKey("AppointmentId");

                    b.HasOne("WebAppMedicalAssistant_DataBase.Entities.MedicalInstitution", "MedicalInstitution")
                        .WithMany("Analyses")
                        .HasForeignKey("MedicalInstitutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAppMedicalAssistant_DataBase.Entities.User", "User")
                        .WithMany("Analyzes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("MedicalInstitution");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.Appointment", b =>
                {
                    b.HasOne("WebAppMedicalAssistant_DataBase.Entities.DoctorVisit", "DoctorVisit")
                        .WithOne("Appointment")
                        .HasForeignKey("WebAppMedicalAssistant_DataBase.Entities.Appointment", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAppMedicalAssistant_DataBase.Entities.TransferredDisease", "TransferredDisease")
                        .WithMany("Appointments")
                        .HasForeignKey("TransferredDiseaseId");

                    b.Navigation("DoctorVisit");

                    b.Navigation("TransferredDisease");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.DoctorVisit", b =>
                {
                    b.HasOne("WebAppMedicalAssistant_DataBase.Entities.Doctor", "Doctor")
                        .WithMany("DoctorVisits")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAppMedicalAssistant_DataBase.Entities.MedicalInstitution", "MedicalInstitution")
                        .WithMany("DoctorVisits")
                        .HasForeignKey("MedicalInstitutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAppMedicalAssistant_DataBase.Entities.TransferredDisease", "TransferredDisease")
                        .WithMany()
                        .HasForeignKey("TransferredDiseaseId");

                    b.HasOne("WebAppMedicalAssistant_DataBase.Entities.User", "User")
                        .WithMany("DoctorVisits")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("MedicalInstitution");

                    b.Navigation("TransferredDisease");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.Fluorography", b =>
                {
                    b.HasOne("WebAppMedicalAssistant_DataBase.Entities.MedicalInstitution", "MedicalInstitution")
                        .WithMany("Fluorographys")
                        .HasForeignKey("MedicalInstitutionId");

                    b.HasOne("WebAppMedicalAssistant_DataBase.Entities.User", "User")
                        .WithMany("Fluorographies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MedicalInstitution");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.MedicalExamination", b =>
                {
                    b.HasOne("WebAppMedicalAssistant_DataBase.Entities.Appointment", "Appointment")
                        .WithMany("MedicalExaminations")
                        .HasForeignKey("AppointmentId");

                    b.HasOne("WebAppMedicalAssistant_DataBase.Entities.MedicalInstitution", "MedicalInstitution")
                        .WithMany("medicalExaminations")
                        .HasForeignKey("MedicalInstitutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAppMedicalAssistant_DataBase.Entities.User", "User")
                        .WithMany("MedicalExaminations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("MedicalInstitution");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.PhysicalTherapy", b =>
                {
                    b.HasOne("WebAppMedicalAssistant_DataBase.Entities.Appointment", "Appointment")
                        .WithMany("PhysicalTherapys")
                        .HasForeignKey("AppointmentId");

                    b.HasOne("WebAppMedicalAssistant_DataBase.Entities.MedicalInstitution", "MedicalInstitution")
                        .WithMany("physicalTherapies")
                        .HasForeignKey("MedicalInstitutionId");

                    b.HasOne("WebAppMedicalAssistant_DataBase.Entities.User", "User")
                        .WithMany("PhysicalTherapies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("MedicalInstitution");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.PrescribedMedication", b =>
                {
                    b.HasOne("WebAppMedicalAssistant_DataBase.Entities.Appointment", "Appointment")
                        .WithMany("PrescribedMedications")
                        .HasForeignKey("AppointmentId");

                    b.HasOne("WebAppMedicalAssistant_DataBase.Entities.Medicine", "Medicine")
                        .WithMany("PrescribedMedications")
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAppMedicalAssistant_DataBase.Entities.User", "User")
                        .WithMany("PrescribedMedication")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Medicine");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.TransferredDisease", b =>
                {
                    b.HasOne("WebAppMedicalAssistant_DataBase.Entities.Disease", "Disease")
                        .WithMany("TransferredDiseases")
                        .HasForeignKey("DiseaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAppMedicalAssistant_DataBase.Entities.User", "User")
                        .WithMany("TransferredDiseases")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Disease");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.User", b =>
                {
                    b.HasOne("WebAppMedicalAssistant_DataBase.Entities.Role", "Roles")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.Vaccination", b =>
                {
                    b.HasOne("WebAppMedicalAssistant_DataBase.Entities.MedicalInstitution", "MedicalInstitutions")
                        .WithMany("Vaccinations")
                        .HasForeignKey("MedicalInstitutionId");

                    b.HasOne("WebAppMedicalAssistant_DataBase.Entities.User", "User")
                        .WithMany("Vaccinations")
                        .HasForeignKey("UserId");

                    b.Navigation("MedicalInstitutions");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.Appointment", b =>
                {
                    b.Navigation("Analysis");

                    b.Navigation("MedicalExaminations");

                    b.Navigation("PhysicalTherapys");

                    b.Navigation("PrescribedMedications");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.Disease", b =>
                {
                    b.Navigation("TransferredDiseases");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.Doctor", b =>
                {
                    b.Navigation("DoctorVisits");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.DoctorVisit", b =>
                {
                    b.Navigation("Appointment")
                        .IsRequired();
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.MedicalInstitution", b =>
                {
                    b.Navigation("Analyses");

                    b.Navigation("DoctorVisits");

                    b.Navigation("Fluorographys");

                    b.Navigation("Vaccinations");

                    b.Navigation("medicalExaminations");

                    b.Navigation("physicalTherapies");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.Medicine", b =>
                {
                    b.Navigation("PrescribedMedications");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.TransferredDisease", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("WebAppMedicalAssistant_DataBase.Entities.User", b =>
                {
                    b.Navigation("Analyzes");

                    b.Navigation("DoctorVisits");

                    b.Navigation("Fluorographies");

                    b.Navigation("MedicalExaminations");

                    b.Navigation("PhysicalTherapies");

                    b.Navigation("PrescribedMedication");

                    b.Navigation("TransferredDiseases");

                    b.Navigation("Vaccinations");
                });
#pragma warning restore 612, 618
        }
    }
}
