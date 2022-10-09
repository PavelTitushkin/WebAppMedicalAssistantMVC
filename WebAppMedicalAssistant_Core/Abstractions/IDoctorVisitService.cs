using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Core.Abstractions
{
    public interface IDoctorVisitService
    {
        Task<List<DoctorVisitDto>> GetAllDoctorVisitAsync(int userId);
    }
}
