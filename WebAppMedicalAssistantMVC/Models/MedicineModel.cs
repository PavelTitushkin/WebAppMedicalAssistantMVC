namespace WebAppMedicalAssistantMVC.Models
{
    public class MedicineModel
    {
        public int Id { get; set; }
        public string NameOfMedicine { get; set; }
        public string ReleaseForm { get; set; }
        public string DosageForm { get; set; }
        public string WayOfIntroduction { get; set; }
        public string LinkToInstructions { get; set; }
        public string ReturnUrl { get; set; }
    }
}
