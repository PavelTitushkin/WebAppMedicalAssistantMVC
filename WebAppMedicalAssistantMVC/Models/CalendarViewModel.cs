using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistantMVC.Models
{
    public class CalendarViewModel
    {
        public List<DoctorVisitDto?>? DoctorVisits { get; set; }
        public List<AnalysisDto?>? AnalysisVisits { get; set;}
        public List<MedicalExaminationDto?>? MedicalExaminationVisits { get; set; }
        public List<PhysicalTherapyDto?>? PhysicalTherapyVisits { get;set; }
    }
}
