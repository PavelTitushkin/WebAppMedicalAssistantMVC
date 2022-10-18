using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistantMVC.Models
{
    public class AppointmentModel
    {
        public int Id { get; set; }
        public string? DescriptionOfDestination { get; set; }


        public List<MedicalExaminationDto>? MedicalExaminationsDto { get; set; }
        public List<PhysicalTherapyDto>? PhysicalTherapysDto { get; set; }
        public List<PrescribedMedicationDto>? PrescribedMedicationsDto { get; set; }
        public List<AnalysisDto>? AnalysisDto { get; set; }
        public int? DoctorVisitId { get; set; }
    }
}
