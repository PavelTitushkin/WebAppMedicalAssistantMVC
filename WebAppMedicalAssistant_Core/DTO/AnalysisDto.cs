namespace WebAppMedicalAssistant_Core.DTO
{
    public class AnalysisDto
    {
        public int Id { get; set; }
        public string? NameOfAnalysis { get; set; }
        public DateTime DateOfAnalysis { get; set; }

        public int UserId { get; set; }
        public int? AppointmentId { get; set; }
        public int? MedicalInstitutionId { get; set; }
        public MedicalInstitutionDto? MedicalInstitution { get; set; }
        public List<ScanOfAnalysisDocumentDto?> ScanOfAnalysisDocument { get; set; }
    }
}
