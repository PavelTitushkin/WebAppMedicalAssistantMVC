namespace WebAppMedicalAssistant_Core.DTO
{
    public class FluorographyDto
    {
        public int Id { get; set; }
        public DateTime DataOfExamination { get; set; }
        public DateTime EndDateOfSurvey { get; set; }
        public string NumberFluorography { get; set; }
        public bool Result { get; set; }

        public int UserId { get; set; }
        public int? MedicalInstitutionId { get; set; }
        public MedicalInstitutionDto? MedicalInstitution { get; set; }
    }
}
