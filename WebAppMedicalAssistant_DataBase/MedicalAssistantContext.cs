using Microsoft.EntityFrameworkCore;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistant_DataBase
{
    public class MedicalAssistantContext : DbContext
    {
        public DbSet<Analysis> Analyses { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorVisit> DoctorVisits { get; set; }
        public DbSet<Fluorography> Fluorographies { get; set; }
        public DbSet<MedicalExamination> MedicalExaminations { get; set; }
        public DbSet<MedicalInstitution> MedicalInstitutions { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<PrescribedMedication> PrescribedMedications { get; set; }
        public DbSet<TransferredDisease> TransferredDiseases { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vaccination> Vaccinations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ScanOfAnalysisDocument> ScanOfAnalysisDocuments { get; set; }
        public DbSet<ScanOfMedicalExamination> ScanOfMedicalExaminations { get; set; }
        public DbSet<RefreshTokens> RefreshTokens { get; set; }

        public MedicalAssistantContext(DbContextOptions<MedicalAssistantContext> options) : base(options)
        {

        }
    }
}
