namespace WebAppMedicalAssistant_Core.DTO
{
    public class PhysicalTherapyDto
    {
        public int Id { get; set; }
        public string? NameOfPhysicalTherapy { get; set; }
        public DateTime StartPhysicalTherapy { get; set; }
        public DateTime EndPhysicalTherapy { get; set; }

        public AppointmentDto? AppointmentDto { get; set; }
        public TransferredDiseaseDto? TransferredDiseaseDto { get; set; }
        public MedicalInstitutionDto? MedicalInstitutionDto { get; set; }

    }
}
