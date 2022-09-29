namespace WebAppMedicalAssistant_Core.DTO
{
    public class FluorographyDto
    {
        public DateTime DataOfExamination { get; set; }
        public DateTime EndDateOfSurvey { get; set; }
        public string? NumberFluorography { get; set; }
        public bool Result { get; set; }

        public MedicalInstitutionDto? MedicalInstitutionDto { get; set; }
    }
}
