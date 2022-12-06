namespace WebAppMedicalAssistantMVC.Models
{
    public class ScanOfMedicalExaminationModel
    {
        public int Id { get; set; }
        public byte[]? ScanOfMedicalExaminationByte { get; set; }
        public int MedicalExaminationId { get; set; }
        public IFormFile? ScanOfMedicalExamination { get; set; }
        public string NameOfMedicalExamination { get; set; }
        public DateTime DateOfMedicalExamination { get; set; }
    }
}
