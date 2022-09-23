namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class PhysicalTherapy : IBaseEntity
    {
        public int Id { get; set; }
        public string NameOfPhysicalTherapy { get; set; }
        public DateTime StartPhysicalTherapy { get; set; }
        public DateTime EndPhysicalTherapy { get; set; }

        public virtual Appointment Appointment { get; set; }
        public int? AppointmentId { get; set; }
        public virtual TransferredDisease TransferredDisease { get; set; }
        public int? TransferredDiseaseId { get; set; }
    }
}
