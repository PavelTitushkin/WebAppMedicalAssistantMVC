namespace WebAppMedicalAssistant_Core.DTO
{
    public class MedicineDto
    {
        public int Id { get; set; }
        public string? NameOfMedicine { get; set; }
        public string? ShortDescriptionOfMedicine { get; set; }

        public List<PrescribedMedicationDto>? PrescribedMedicationsDto { get; set; }
    }
}
