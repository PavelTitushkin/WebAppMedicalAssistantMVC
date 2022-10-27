using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_Core;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppMedicalAssistantMVC.Models
{
    public class TransferredDiseaseModel
    {
        public DateTime DateOfDisease { get; set; }
        public DateTime? DateOfRecovery { get; set; }
        public bool TypeOfTreatment { get; set; }
        public FormOfTransferredDisease FormOfTransferredDiseaseList { get; set; }
        
        public string? NameOfDisease { get; set; }
        public int DiseaseId { get; set; }
        public SelectList DiseaseList { get; set; }
        public int UserId { get; set; }
        public int? AppointmentId { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
