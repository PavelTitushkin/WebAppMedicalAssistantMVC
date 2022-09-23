namespace WebAppMedicalAssistant_Core.DTO
{
    public class TransferredDiseaseDto
    {
        public int Id { get; set; }
        public DateTime DateOfDisease { get; set; }
        public DateTime? DateOfRecovery { get; set; }
        public bool TypeOfTreatment { get; set; }
        public FormOfTransferredDisease FormOfTransferredDiseaseDto { get; set; }

        public List<DiseaseDto> DiseasesDto { get; set; }
        public List<PrescribedMedicationDto> PrescribedMedicationsDto { get; set; }
        public List<PhysicalTherapyDto> PhysicalTherapiesDto { get; set; }
        public DoctorVisitDto DoctorVisitDto { get; set; }
        public UserDto UserDto { get; set; }
    }
}
