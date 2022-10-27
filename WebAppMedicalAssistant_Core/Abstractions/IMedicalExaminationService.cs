using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Core.Abstractions
{
    public interface IMedicalExaminationService
    {
        Task<List<MedicalExaminationDto>> GetAllMedicalExaminationAsync(int userId);
        Task<int> CreateMedicalExaminationAsync(MedicalExaminationDto dto);
    }
}
