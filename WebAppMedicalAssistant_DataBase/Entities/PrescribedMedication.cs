namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class PrescribedMedication : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime StartDateOfMedication { get; set; }
        public DateTime EndDateOfMedication { get; set; }
        public string MedicineDose { get; set; }
        public decimal MedicinePrice { get; set; }

        public virtual List<Medicine> Medicines { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual TransferredDisease TransferredDisease { get; set; }
        public int? TransferredDiseaseId { get; set; }
        public virtual Appointment Appointment { get; set; }
        public int? AppointmentId { get; set; }
    }
}
