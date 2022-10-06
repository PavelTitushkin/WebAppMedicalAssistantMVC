using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Core.Abstractions
{
    public interface IFluorographyService
    {
        Task<List<FluorographyDto>> GetAllFluorographiesAsync(int userId);
    }
}
