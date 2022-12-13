using WebAppMedicalAssistant_Core.Abstractions.EmailService;
using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Core.Abstractions
{
    public interface IAdminService
    {
        Task<List<Message?>> GetUsersWithNearestDoctorVisitAsync();
    }
}
