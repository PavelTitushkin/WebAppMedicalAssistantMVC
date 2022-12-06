namespace WebAppMedicalAssistant_Core.DTO
{
    public class PhysicalTherapyDto
    {
        public int Id { get; set; } 
        public string? NameOfPhysicalTherapy { get; set; }
        public DateTime DatePhysicalTherapy { get; set; }

        public int UserId { get; set; }
        public int? AppointmentId { get; set; }
        public MedicalInstitutionDto? MedicalInstitution { get; set; }
        public int? MedicalInstitutionId { get; set; }
    }
}
