using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Core.Abstractions
{
    public interface IAnalysisService
    {
        Task<IOrderedQueryable<AnalysisDto>> GetAllAnalysisAsync(int userId);
        Task<AnalysisDto> GetAnalysisByIdAsync(int id);
        Task<List<AnalysisDto>> GetPeriodAnalysisAsync(DateTime SearchDateStart, DateTime SearchDateEnd, int userId);
        Task<int> CreateAnalysisAsync(AnalysisDto analysisDto);
        Task<int> CreateScanOfDocumentsAnalysisAsync(ScanOfAnalysisDocumentDto dto);
        Task<int> UpdateAnalysisAsync(AnalysisDto dto, int id);
        Task<int> UpdateScanOfDocumentsAnalysisAsync(ScanOfAnalysisDocumentDto dto);
        Task DeleteAnalysisAsync(int id);
        Task DeleteScanOfAnalysisAsync(int id);
        Task<ScanOfAnalysisDocumentDto?> GetScanOfAnalysisByIdAsync(int id);
    }
}
