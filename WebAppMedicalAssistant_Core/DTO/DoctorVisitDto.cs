namespace WebAppMedicalAssistant_Core.DTO
{
    public class DoctorVisitDto
    {
        public int Id { get; set; }
        public DateTime DateVisit { get; set; }
        public decimal PriceVisit { get; set; }

        public AppointmentDto Appointment { get; set; }
        public MedicalInstitutionDto MedicalInstitution { get; set; }
        public int MedicalInstitutionId { get; set; }
        public DoctorDto Doctor { get; set; }
        public int DoctorId { get; set; }
        public TransferredDiseaseDto? TransferredDisease { get; set; }
        public int? TransferredDiseaseId { get; set; }

        public int AppointmentId { get; set; }
        public DiseaseDto? DiseaseDto { get; set; }
        public int UserDtoId { get; set; }
    }
}
