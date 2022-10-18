namespace WebAppMedicalAssistant_Core.DTO
{
    public class TransferredDiseaseDto
    {
        public int Id { get; set; }
        public DateTime DateOfDisease { get; set; }
        public DateTime? DateOfRecovery { get; set; }
        public bool TypeOfTreatment { get; set; }
        public FormOfTransferredDisease FormOfTransferredDiseaseDto { get; set; }
        public string? NameOfDisease { get; set; }
    }
}
