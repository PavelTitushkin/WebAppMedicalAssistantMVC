using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebAppMedicalAssistantMVC.Models
{
    public class FluorographyModel
    {
        public int Id { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataOfExamination { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDateOfSurvey { get; set; }
        public string NumberFluorography { get; set; }
        public bool Result { get; set; }

        public int UserId { get; set; }
        public int? MedicalInstitutionId { get; set; }
        public SelectList MedicalInstitutionList { get; set; }
    }
}
