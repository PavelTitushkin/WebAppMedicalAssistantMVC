using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebAppMedicalAssistantAPI.Model.Requests
{
    public class DoctorVisitRequestModel
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateVisit { get; set; }
        //public string? NameMedicalInstitution { get; set; }
        //public string? NameOfDisease { get; set; }

        public int? TransferredDiseaseId { get; set; }
        public decimal PriceVisit { get; set; }
        public int MedicalInstitutionId { get; set; }
        //public SelectList? MedicalInstitutionList { get; set; }
        public int DoctorId { get; set; }
        //public SelectList? DoctorList { get; set; }
        public int UserDtoId { get; set; }
        //public string? ReturnUrl { get; set; }
    }
}
