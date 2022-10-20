namespace WebAppMedicalAssistant_Core.DTO
{
    public class PhysicalTherapyDto
    {
        public string? NameOfPhysicalTherapy { get; set; }
        public DateTime StartPhysicalTherapy { get; set; }
        public DateTime? EndPhysicalTherapy { get; set; }

        public int? AppointmentId { get; set; }
        public int MedicalInstitutionId { get; set; }
    }
}
