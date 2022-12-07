using System.Reflection;
using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Core.Abstractions
{
    public interface IVaccinationService
    {
        Task<List<VaccinationDto>> GetAllVaccinationsAsync(int userId);
        Task<List<VaccinationDto>> GetPeriodVaccinationAsync(DateTime searchDateStart, DateTime searchDateEnd, int id);
        Task<VaccinationDto> GetVaccinationByIdAsync(int id);
        Task<int> CreateVaccinationAsync(VaccinationDto dto);
        Task<int> UpdateVaccinationAsync(VaccinationDto dto, int id);
        Task DeleteVaccinationAsync(int id);
    }
}
