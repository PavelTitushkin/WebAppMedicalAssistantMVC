using System.Numerics;

namespace WebAppMedicalAssistant_Core.DTO
{
    public class DoctorVisitDto
    {
        public int Id { get; set; }
        public DateTime DateVisit { get; set; }
        public decimal PriceVisit { get; set; }

        public int? TransferredDiseaseId { get; set; }
        public int AppointmentDtoId { get; set; }
        public AppointmentDto AppointmentDto { get; set; }
        public MedicalInstitutionDto MedicalInstitutionDto { get; set; }
        public int MedicalInstitutionDtoId { get; set; }
        public DoctorDto DoctorDto { get; set; }
        public int DoctorDtoId { get; set; }
        public string? NameOfDisease { get; set; }
        public int UserDtoId { get; set; } 
    }
}
