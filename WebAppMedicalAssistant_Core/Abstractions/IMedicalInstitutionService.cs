using System.Reflection;
using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Core.Abstractions
{
    public interface IMedicalInstitutionService
    {
        Task<List<MedicalInstitutionDto>> GetMedicalInstitutionsAsync();
        Task<MedicalInstitutionDto> GetByIdMedicalInstitutionAsync(int id);
        Task<int> CreateMedicalInstitutionAsync(MedicalInstitutionDto dto);
        Task<int> UpdateMedicalInstitutionAsync(MedicalInstitutionDto dto, int id);
        Task DeleteMedicalInstitutionAsync(int id);
    }
}
