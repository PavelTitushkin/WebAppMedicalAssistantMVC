namespace WebAppMedicalAssistant_Core.DTO
{
    public class MedicineDto
    {
        public int Id { get; set; }
        public string NameOfMedicine { get; set; }
        public string ShortDescriptionOfMedicine { get; set; }

        public PrescribedMedicationDto PrescribedMedicationDto { get; set; }
    }
}
