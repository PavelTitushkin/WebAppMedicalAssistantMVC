namespace WebAppMedicalAssistant_Core.DTO
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string LastNameDoctor { get; set; }
        public string FirstNameDoctor { get; set; }
        public string PatronymicDoctor { get; set; }
        public string Specializacion { get; set; }
        public float? Rating { get; set; }

        public List<DoctorVisitDto> DoctorVisitsDto { get; set; }
    }
}
