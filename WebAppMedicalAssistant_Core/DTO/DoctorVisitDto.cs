namespace WebAppMedicalAssistant_Core.DTO
{
    public class DoctorVisitDto
    {
        public int Id { get; set; }
        public DateTime DateVisit { get; set; }
        public decimal PriceVisit { get; set; }

        //навигационные свойства
        public List<MedicalInstitutionDto> MedicalInstitutionsDto { get; set; }
        public List<DoctorDto> DoctorsDto { get; set; }
        public List<AppointmentDto> AppointmentsDto { get; set; }
        public List<TransferredDiseaseDto> TransferredDiseasesDto { get; set; }
        public UserDto UserDto { get; set; }
    }
}
