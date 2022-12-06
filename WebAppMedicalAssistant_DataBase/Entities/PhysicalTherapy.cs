namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class PhysicalTherapy : IBaseEntity
    {
        public int Id { get; set; }
        public string NameOfPhysicalTherapy { get; set; }
        public DateTime DatePhysicalTherapy { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
        public Appointment? Appointment { get; set; }
        public int? AppointmentId { get; set; }
        public MedicalInstitution? MedicalInstitution { get; set; }
        public int? MedicalInstitutionId { get; set; }
    }
}
