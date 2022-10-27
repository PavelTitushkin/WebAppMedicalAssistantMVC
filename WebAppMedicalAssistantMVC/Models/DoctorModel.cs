namespace WebAppMedicalAssistantMVC.Models
{
    public class DoctorModel
    {
        public string FullNameDoctor { get; set; }
        public string Specializacion { get; set; }
        public float? Rating { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
