using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistantMVC.Models
{
    public class PhysicalTherapyModel
    {
        public int Id { get; set; }
        public string NameOfPhysicalTherapy { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatePhysicalTherapy { get; set; }
        public int UserId { get; set; }
        public int? MedicalInstitutionId { get; set; }
        public SelectList MedicalInstitutionList { get; set; }
        public int? AppointmentId { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
