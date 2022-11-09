using System.Reflection;
using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Core.Abstractions
{
    public interface IMedicineService
    {
        Task<List<MedicineDto>> GetAllMedicineAsync();
        List<string>? SearchMedicineInTabletkaBy(string nameOfMedicine);
        Task<int> AddMedicineAsync(List<string>? listLinkMedicine);
        Task<MedicineDto> GetByIdMedicineAsync(int id);
        Task<int> UpdateMedicineAsync(MedicineDto dto, int id);
        Task DeleteMedicineAsync(int id);
    }
}
