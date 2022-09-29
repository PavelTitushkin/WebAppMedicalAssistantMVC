namespace WebAppMedicalAssistant_Core.DTO
{
    public class DoctorVisitDto
    {
        public int Id { get; set; }
        public DateTime DateVisit { get; set; }
        public decimal PriceVisit { get; set; }

        //навигационные свойства
        public MedicalInstitutionDto MedicalInstitutionDto { get; set; }
        public DoctorDto DoctorDto { get; set; }
        public List<AppointmentDto> AppointmentsDto { get; set; }
        public TransferredDiseaseDto TransferredDiseaseDto { get; set; }
        public UserDto UserDto { get; set; }
    }
}
