namespace WebAppMedicalAssistant_Core.DTO
{
    public class PrescribedMedicationDto
    {
        public int Id { get; set;}
        public DateTime StartDateOfMedication { get; set; }
        public DateTime? EndDateOfMedication { get; set; }
        public string? MedicineDose { get; set; }
        public decimal? MedicinePrice { get; set; }

        public MedicineDto Medicine { get; set; }
        public int MedicineId { get; set; }
        public int UserId { get; set; }
        public int? AppointmentId { get; set; }
    }
}
