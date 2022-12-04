using System.Reflection;
using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Core.Abstractions
{
    public interface IFluorographyService
    {
        Task<List<FluorographyDto>> GetAllFluorographiesAsync(int userId);
        Task<FluorographyDto> GetFluorographyByIdAsync(int id);
        Task<List<FluorographyDto>> GetPeriodfluorographyAsync(DateTime SearchDateStart, DateTime SearchDateEnd, int userId);
        Task<int> CreatefluorographyAsync(FluorographyDto fluorographyDto);
        Task<int> UpdateFluorographyAsync(FluorographyDto dto, int id);
        Task DeleteFluorographyAsync(int id);
    }
}
