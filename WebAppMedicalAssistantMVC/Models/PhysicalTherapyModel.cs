using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistantMVC.Models
{
    public class PhysicalTherapyModel
    {
        public string NameOfPhysicalTherapy { get; set; }
        public DateTime StartPhysicalTherapy { get; set; }
        public DateTime EndPhysicalTherapy { get; set; }

        public int MedicalInstitutionId { get; set; }
        public SelectList MedicalInstitutionList { get; set; }
        public int? AppointmentId { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
