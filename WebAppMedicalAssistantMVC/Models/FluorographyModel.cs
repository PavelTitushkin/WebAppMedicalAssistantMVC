using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppMedicalAssistantMVC.Models
{
    public class FluorographyModel
    {
        public DateTime DataOfExamination { get; set; }
        public DateTime? EndDateOfSurvey { get; set; }
        public string NumberFluorography { get; set; }
        public bool Result { get; set; }

        public int UserId { get; set; }
        public int MedicalInstitutionId { get; set; }
        public SelectList MedicalInstitutionList { get; set; }
    }
}
