using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebAppMedicalAssistantMVC.Models
{
    public class PrescribedMedicationModel
    {
        public int Id { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDateOfMedication { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDateOfMedication { get; set; }
        public string? MedicineDose { get; set; }
        public decimal? MedicinePrice { get; set; }

        public int UserId { get; set; }
        public int MedicineId { get; set; }
        public SelectList MedicineList { get; set; }
        public int? AppointmentId { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
