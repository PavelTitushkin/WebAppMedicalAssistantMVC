using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Core.Abstractions
{
    public interface IDoctorVisitService
    {
        Task<List<DoctorVisitDto?>> GetScheduledDoctorVisitAsync(DateTime dateNow, int id);
        Task<List<DoctorVisitDto>> GetAllDoctorVisitAsync(int userId);
        Task<List<DoctorVisitDto>> GetPeriodDoctorVisitAsync(DateTime SearchDateStart, DateTime SearchDateEnd, int userDtoId);
        Task<List<DoctorVisitDto>> GetDoctorVisitByIdTransferredDiseaseAsync(int id);
        Task<int> CreateDoctorVisitAsync(DoctorVisitDto doctorVisitDto);
        Task<AppointmentDto> GetAppointmentAsync(int doctorVisitId);
        Task<int> CreateAppointment(int id);
        Task<DoctorVisitDto> GetDoctorVisitByIdAsync(int? appontmentId);
        Task<int> UpdateDoctorVisitAsync(DoctorVisitDto dto, int dtoId);
        Task<int> UpdateAppointmentAsync(AppointmentDto dto,int dtoId);
        Task DeleteDoctorVisitAsync(int id);
    }
}
