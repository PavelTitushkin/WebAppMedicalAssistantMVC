using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistantMVC.Models
{
    public class AnalysisModel
    {
        public int Id { get; set; }
        public string NameOfAnalysis { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfAnalysis { get; set; }
        public List<IFormFile?>? ScanOfAnalysisDocument { get; set; }
        public int CountList { get; set; }
        public IFormFile? ScanOfAnalysisOne { get; set; }
        public IFormFile? ScanOfAnalysisTwo { get; set; }
        public IFormFile? ScanOfAnalysisThree { get; set; }
        public IFormFile? ScanOfAnalysisFour { get; set; }
        public IFormFile? ScanOfAnalysisFive { get; set; }

        public int UserId { get; set; }
        public int? AppointmentId { get; set; }
        public int MedicalInstitutionId { get; set; }
        public SelectList MedicalInstitutionList { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
