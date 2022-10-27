namespace WebAppMedicalAssistant_Core.DTO
{
    public class MedicalExaminationDto
    {
        public string NameOfMedicalExamination { get; set; }
        public DateTime DateOfMedicalExamination { get; set; }
        public decimal? PriceOfMedicalExamination { get; set; }
        public byte[]? ScanOfMedicalExamination { get; set; }

        public int? UserId { get; set; }
        public int? AppointmentId { get; set; }
        public int? MedicalInstitutionId { get; set; }
        public MedicalInstitutionDto? MedicalInstitution { get; set; }
    }
}
