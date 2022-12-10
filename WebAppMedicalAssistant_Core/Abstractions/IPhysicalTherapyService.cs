using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Core.Abstractions
{
    public interface IPhysicalTherapyService
    {
        Task<List<PhysicalTherapyDto?>> GetScheduledPhysicalTherapyAsync(DateTime dateNow, int id);
        Task<List<PhysicalTherapyDto>> GetAllPhysicalTherapyAsync(int id);
        Task<List<PhysicalTherapyDto>> GetPeriodPhysicalTherapyAsync(DateTime SearchDateStart, DateTime SearchDateEnd, int id);
        Task<PhysicalTherapyDto> GetPhysicalTherapyByIdAsync(int id);
        Task<int> CreatePhysicalTherapyAsync(PhysicalTherapyDto dto);
        Task<int> UpdatePhysicalTherapyAsync(PhysicalTherapyDto dto, int id);
        Task DeletePhysicalTherapyAsync(int id);
    }
}
