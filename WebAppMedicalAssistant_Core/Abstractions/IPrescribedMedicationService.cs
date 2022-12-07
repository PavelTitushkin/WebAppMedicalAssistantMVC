using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Core.Abstractions
{
    public interface IPrescribedMedicationService
    {
        Task<List<PrescribedMedicationDto>> GetAllPrescribedMedicationsAsync(int id);
        Task<List<PrescribedMedicationDto>> GetPeriodPrescribedMedicationAsync(DateTime searchDateStart, DateTime searchDateEnd, int id);
        Task<PrescribedMedicationDto> GetPrescribedMedicationByIdAsync(int id);
        Task<int> CreatePrescribedMedicationAsync(PrescribedMedicationDto dto);
    }
}
