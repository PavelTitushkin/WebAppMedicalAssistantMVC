namespace WebAppMedicalAssistant_Core.DTO
{
    public class PrescribedMedicationDto
    {
        public int Id { get; set; }
        public DateTime StartDateOfMedication { get; set; }
        public DateTime EndDateOfMedication { get; set; }
        public string? MedicineDose { get; set; }
        public decimal MedicinePrice { get; set; }

        public List<DiseaseDto>? DiseasesDto { get; set; }
        public MedicineDto? MedicinesDto { get; set; }
        public UserDto? UserDto { get; set; }
        public AppointmentDto? AppointmentDto { get; set; }
    }
}
