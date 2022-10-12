using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Core.Abstractions
{
    public interface IAnalysisService
    {
        Task<List<AnalysisDto>> GetAllAnalysisAsync(int userId);
        Task<List<MedicalInstitutionDto>> GetMedicalInstitutionsAsync();
        Task<int> CreateAnalysisAsync(AnalysisDto analysisDto);

    }
}
