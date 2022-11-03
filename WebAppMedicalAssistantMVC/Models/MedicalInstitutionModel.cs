namespace WebAppMedicalAssistantMVC.Models
{
    public class MedicalInstitutionModel
    {
        public int Id { get; set; }
        public string NameMedicalInstitution { get; set; }
        public string Adress { get; set; }
        public string? OperatingMode { get; set; }
        public string? Contact { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
