using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Core.Abstractions
{
    public interface IAnalysisService
    {
        Task<List<AnalysisDto>> GetAllAnalysisAsync(int userId);
    }
}
