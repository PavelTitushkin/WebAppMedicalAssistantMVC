using WebAppMedicalAssistant_Data.Abstractions;
using WebAppMedicalAssistant_Data.Abstractions.Repositories;
using WebAppMedicalAssistant_DataBase;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistant_Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MedicalAssistantContext _database;

        public IRepository<Analysis> Analysis { get; }
        public IRepository<Appointment> Appointment { get; }
        public IRepository<Disease> Disease { get; }
        public IRepository<Doctor> Doctor { get; }
        public IRepository<DoctorVisit> DoctorVisit { get; }
        public IRepository<Fluorography> Fluorography { get; }
        public IRepository<MedicalExamination> MedicalExamination { get; }
        public IRepository<MedicalInstitution> MedicalInstitution { get; }
        public IRepository<Medicine> Medicine { get; }
        public IRepository<PhysicalTherapy> PhysicalTherapy { get; }
        public IRepository<PrescribedMedication> PrescribedMedication { get; }
        public IRepository<TransferredDisease> TransferredDisease { get; }
        public IRepository<User> User { get; }
        public IRepository<Vaccination> Vaccination { get; }
        public IRepository<Role> Roles { get; }
        public IRepository<ScanOfAnalysisDocument> ScanOfAnalysisDocument { get; }
        public IRepository<ScanOfMedicalExamination> ScanOfMedicalExamination { get; }

        public UnitOfWork(MedicalAssistantContext database, IRepository<Analysis> analysis, IRepository<Appointment> appointment, IRepository<Disease> disease, IRepository<Doctor> doctor, IRepository<DoctorVisit> doctorVisit, IRepository<Fluorography> fluorography, IRepository<MedicalExamination> medicalExamination, IRepository<MedicalInstitution> medicalInstitution, IRepository<Medicine> medicine, IRepository<PhysicalTherapy> physicalTherapy, IRepository<PrescribedMedication> prescribedMedication, IRepository<TransferredDisease> transferredDisease, IRepository<User> user, IRepository<Vaccination> vaccination, IRepository<Role> role, IRepository<ScanOfAnalysisDocument> scanOfAnalysisDocument, IRepository<ScanOfMedicalExamination> scanOfMedicalExamination)
        {
            _database = database;
            Analysis = analysis;
            Appointment = appointment;
            Disease = disease;
            Doctor = doctor;
            DoctorVisit = doctorVisit;
            Fluorography = fluorography;
            MedicalExamination = medicalExamination;
            MedicalInstitution = medicalInstitution;
            Medicine = medicine;
            PhysicalTherapy = physicalTherapy;
            PrescribedMedication = prescribedMedication;
            TransferredDisease = transferredDisease;
            User = user;
            Vaccination = vaccination;
            Roles = role;
            ScanOfAnalysisDocument = scanOfAnalysisDocument;
            ScanOfMedicalExamination = scanOfMedicalExamination;
        }

        public async Task<int> Commit()
        {
            return await _database.SaveChangesAsync();
        }
    }
}
