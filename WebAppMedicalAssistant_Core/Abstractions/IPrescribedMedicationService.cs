using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Core.Abstractions
{
    public interface IPrescribedMedicationService
    {
        Task<List<PrescribedMedicationDto>> GetAllPrescribedMedicationsAsync(int id);
    }
}
