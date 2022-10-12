using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistantMVC.Models
{
    public class AnalysisModel
    {
        public string NameOfAnalysis { get; set; }
        public DateTime DateOfAnalysis { get; set; }
        public byte[]? ScanOfAnalysisDocument { get; set; }

        public int UserId { get; set; }
        public int MedicalInstitutionId { get; set; }
        public SelectList MedicalInstitutionList { get; set; }
    }
}
