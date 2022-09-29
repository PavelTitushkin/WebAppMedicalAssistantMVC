using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Core.Abstractions
{
    public interface ITransferredDiseaseService
    {
        Task<List<TransferredDiseaseDto>> GetAllTransferredDiseaseAsync();
    }
}
