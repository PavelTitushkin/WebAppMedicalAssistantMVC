using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Core.Abstractions
{
    public interface IDoctorService
    {
        Task<List<DoctorDto>> GetAllDoctorAsync();
        Task<int> CreateDoctorAsync(DoctorDto dto);
        Task<DoctorDto?> GetDoctorByIdAsync(int id);
    }
}
