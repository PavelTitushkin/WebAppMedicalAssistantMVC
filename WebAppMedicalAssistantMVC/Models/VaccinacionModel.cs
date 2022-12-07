using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebAppMedicalAssistantMVC.Models
{
    public class VaccinacionModel
    {
        public int Id { get; set; }
        public string? ApplicationOfVaccine { get; set; }
        public string? NameOfVaccine { get; set; }
        public string? VacineDose { get; set; }
        public string? VacineSeria { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfVaccination { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? VaccinationExpirationDate { get; set; }

        public int UserId { get; set; }
        public int? MedicalInstitutionId { get; set; }
        public SelectList MedicalInstitutionList { get; set; }
        public string? ReturnUrl { get; set; }

    }
}
