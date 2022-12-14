using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Core.Abstractions
{
    public interface ITransferredDiseaseService
    {
        Task<List<TransferredDiseaseDto>> GetAllTransferredDiseaseAsync(int userId);
        Task <TransferredDiseaseDto?> GetTransferredDiseaseByIdAsync(int id);
        Task<List<TransferredDiseaseDto>> GetPeriodtransferredDiseaseAsync(DateTime SearchDateStart, DateTime SearchDateEnd, int userDtoId);
        Task<List<DiseaseDto>> GetAllDiseaseAsync();
        Task <int> CreateTransferredDiseaseAsync(TransferredDiseaseDto dto);
        Task<int> CreateDiseaseAsync(DiseaseDto dto);
        Task<int> UpdateTransferredDiseaseAsync(TransferredDiseaseDto? dto, int dtoId);
        Task DeleteTransferredDiseaseAsync(int id);
    }
}
