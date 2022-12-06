using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Core.Abstractions
{
    public interface IMedicalExaminationService
    {
        Task<List<MedicalExaminationDto>> GetAllMedicalExaminationAsync(int userId);
        Task<MedicalExaminationDto> GetMedicalExaminationByIdAsync(int id);
        Task<List<MedicalExaminationDto>> GetPeriodMedicalExaminationAsync(DateTime SearchDateStart, DateTime SearchDateEnd, int userId);
        Task<ScanOfMedicalExaminationDto> GetScanOfMedicalExaminationByIdAsync(int id);
        Task<int> CreateMedicalExaminationAsync(MedicalExaminationDto dto);
        Task<int> CreateScanOfMedicalExaminationAsync(ScanOfMedicalExaminationDto dto);
        Task<int> UpdateMedicalExaminationAsync(MedicalExaminationDto dto, int id);
        Task<int> UpdateScanOfMedicalExaminationAsync(ScanOfMedicalExaminationDto dto);
        Task DeleteMedicalExaminationAsync(int id);
    }
}
