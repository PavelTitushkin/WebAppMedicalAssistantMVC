namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class PrescribedMedication : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime StartDateOfMedication { get; set; }
        public DateTime? EndDateOfMedication { get; set; }
        public string? MedicineDose { get; set; }
        public decimal? MedicinePrice { get; set; }

        public Medicine Medicine { get; set; }
        public int MedicineId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Appointment? Appointment { get; set; }
        public int? AppointmentId { get; set; }
    }
}
