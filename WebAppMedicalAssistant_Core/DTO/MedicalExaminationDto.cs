namespace WebAppMedicalAssistant_Core.DTO
{
    public class MedicalExaminationDto
    {
        public int Id { get; set; }
        public string? NameOfMedicalExamination { get; set; }
        public DateTime DateOfMedicalExamination { get; set; }
        public decimal? PriceOfMedicalExamination { get; set; }
        public byte[]? ScanOfMedicalExamination { get; set; }

        public AppointmentDto? AppointmentDto { get; set; }
        public MedicalInstitutionDto? MedicalInstitution { get; set; }

        public UserDto? UserDto { get; set; }
    }
}
