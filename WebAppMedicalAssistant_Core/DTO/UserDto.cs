namespace WebAppMedicalAssistant_Core.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        //навигационные свойства
        public List<AnalysisDto> AnalyzesDto { get; set; }
        public List<DoctorVisitDto> DoctorVisitsDto { get; set; }
        public List<MedicalExaminationDto> MedicalExaminationsDto { get; set; }
        public List<VaccinationDto> VaccinationsDto { get; set; }
        public List<FluorographyDto> FluorographiesDto { get; set; }
        public List<PrescribedMedicationDto> PrescribedMedicationDto { get; set; }
        public List<TransferredDiseaseDto> TransferredDiseasesDto { get; set; }
    }
}
