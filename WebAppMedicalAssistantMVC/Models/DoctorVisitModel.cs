using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebAppMedicalAssistantMVC.Models
{
    public class DoctorVisitModel
    {
        [DataType(DataType.Date)]
        public DateTime DateVisit { get; set; }

        //[DisplayFormat(DataFormatString = "{0:n0}")]
        public decimal PriceVisit { get; set; }
        public int MedicalInstitutionId { get; set; }
        public SelectList MedicalInstitutionList { get; set; }
        public int DoctorId { get; set; }
        public SelectList DoctorList { get; set; }
        public int UserId { get; set; }
        public string? ReturnUrl {get;set;}
    }
}
