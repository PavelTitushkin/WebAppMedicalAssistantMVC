using System.Reflection;
using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Core.Abstractions
{
    public interface IMedicineService
    {
        Task<List<MedicineDto>> GetAllMedicineAsync();
        void AddMedicineAsync(string nameOfMedicine);
    }
}
