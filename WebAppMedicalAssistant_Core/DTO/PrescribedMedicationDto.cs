namespace WebAppMedicalAssistant_Core.DTO
{
    public class PrescribedMedicationDto
    {
        public DateTime StartDateOfMedication { get; set; }
        public DateTime EndDateOfMedication { get; set; }
        public string? MedicineDose { get; set; }
        public decimal MedicinePrice { get; set; }

        public int UserId { get; set; }
        public int? AppointmentId { get; set; }
        public MedicineDto? MedicinesDto { get; set; }
    }
}
