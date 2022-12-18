using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebAppMedicalAssistant_Core;

namespace WebAppMedicalAssistantMVC.Models
{
    public class TransferredDiseaseModel
    {
        public int Id { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfDisease { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfRecovery { get; set; }
        public bool TypeOfTreatment { get; set; }
        public int FormOfTransferredDiseaseListId { get; set; }
        public FormOfTransferredDisease FormOfTransferredDiseaseList { get; set; }
        public string? NameOfDisease { get; set; }
        public int DiseaseId { get; set; }
        public SelectList DiseaseList { get; set; }
        public int UserId { get; set; }
        public int? AppointmentId { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
