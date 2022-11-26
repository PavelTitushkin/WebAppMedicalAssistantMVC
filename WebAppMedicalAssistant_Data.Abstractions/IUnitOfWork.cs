using WebAppMedicalAssistant_Data.Abstractions.Repositories;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistant_Data.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<Analysis> Analysis { get; }
        IRepository<Appointment> Appointment { get; }
        IRepository<Disease> Disease { get; }
        IRepository<Doctor> Doctor { get; }
        IRepository<DoctorVisit> DoctorVisit { get; }
        IRepository<Fluorography> Fluorography { get; }
        IRepository<MedicalExamination> MedicalExamination { get; }
        IRepository<MedicalInstitution> MedicalInstitution { get; }
        IRepository<Medicine> Medicine { get; }
        IRepository<PhysicalTherapy> PhysicalTherapy { get; }
        IRepository<PrescribedMedication> PrescribedMedication { get; }
        IRepository<TransferredDisease> TransferredDisease { get; }
        IRepository<User> User { get; }
        IRepository<Vaccination> Vaccination { get; }
        IRepository<Role> Roles { get; }
        IRepository<ScanOfAnalysisDocument> ScanOfAnalysisDocument { get; }

        Task<int> Commit();
    }
}
