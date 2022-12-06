using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistantMVC.Models
{
    public class MedicalExaminationModel
    {
        public int Id { get; set; }
        public string NameOfMedicalExamination { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfMedicalExamination { get; set; }
        public decimal? PriceOfMedicalExamination { get; set; }
        public List<IFormFile?>? ScanOfMedicalExamination { get; set; }
        public int CountList { get; set; }
        public IFormFile? ScanOfMedicalExaminationOne { get; set; }
        public IFormFile? ScanOfMedicalExaminationTwo { get; set; }
        public IFormFile? ScanOfMedicalExaminationThree { get; set; }
        public IFormFile? ScanOfMedicalExaminationFour { get; set; }
        public IFormFile? ScanOfMedicalExaminationFive { get; set; }

        public int UserId { get; set; }
        public int? AppointmentId { get; set; }
        public int MedicalInstitutionId { get; set; }
        public SelectList MedicalInstitutionList { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
