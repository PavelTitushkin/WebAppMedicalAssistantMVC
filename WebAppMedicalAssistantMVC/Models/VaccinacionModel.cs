using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistantMVC.Models
{
    public class VaccinacionModel
    {
        public string? ApplicationOfVaccine { get; set; }
        public string? NameOfVaccine { get; set; }
        public string? VacineDose { get; set; }
        public string? VacineSeria { get; set; }
        public DateTime DateOfVaccination { get; set; }
        public DateTime? VaccinationExpirationDate { get; set; }

        public int UserId { get; set; }
        public int MedicalInstitutionId { get; set; }
        public SelectList MedicalInstitutionList { get; set; }
        public string? ReturnUrl { get; set; }

    }
}
