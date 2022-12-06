namespace WebAppMedicalAssistant_Core.DTO
{
    public class MedicalExaminationDto
    {
        public int Id { get; set; }
        public string NameOfMedicalExamination { get; set; }
        public DateTime DateOfMedicalExamination { get; set; }
        public decimal? PriceOfMedicalExamination { get; set; }

        public int? UserId { get; set; }
        public int? AppointmentId { get; set; }
        public MedicalInstitutionDto? MedicalInstitution { get; set; }
        public int? MedicalInstitutionId { get; set; }
        public List<ScanOfMedicalExaminationDto?> ScanOfMedicalExaminations { get; set; }

    }
}
