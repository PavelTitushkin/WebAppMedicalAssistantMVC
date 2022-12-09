using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistantMVC.Models
{
    public class CalendarViewModel
    {
        public List<DoctorVisitDto?>? DoctorVisits { get; set; }
        public DateTime DateNow { get; set; }
    }
}
