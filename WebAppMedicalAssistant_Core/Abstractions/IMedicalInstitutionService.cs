using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Core.Abstractions
{
    public interface IMedicalInstitutionService
    {
        Task<List<MedicalInstitutionDto>> GetMedicalInstitutionsAsync();
    }
}
