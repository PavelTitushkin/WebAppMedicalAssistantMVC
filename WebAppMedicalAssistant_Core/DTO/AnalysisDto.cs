namespace WebAppMedicalAssistant_Core.DTO
{
    public class AnalysisDto
    {
        public int Id { get; set; }
        public string? NameOfAnalysis { get; set; }
        public DateTime DateOfAnalysis { get; set; }
        public byte[]? ScanOfAnalysisDocument { get; set; }

        public int UserId { get; set; }
        public int? AppointmentId { get; set; }
        public int MedicalInstitutionDtoId { get; set; }
        public MedicalInstitutionDto? MedicalInstitutionDto { get; set; }
    }
}
