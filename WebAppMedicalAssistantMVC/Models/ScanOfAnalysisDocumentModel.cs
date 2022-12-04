namespace WebAppMedicalAssistantMVC.Models
{
    public class ScanOfAnalysisDocumentModel
    {
        public int Id { get; set; }
        public byte[]? ScanOfAnalysisByte { get; set; }
        public int AnalysisId { get; set; }
        public IFormFile? ScanOfAnalysis { get; set; }
        public string NameOfAnalysis { get; set; }
        public DateTime DateOfAnalysis { get; set; }
    }
}
