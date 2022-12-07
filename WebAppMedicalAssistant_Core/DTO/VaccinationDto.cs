namespace WebAppMedicalAssistant_Core.DTO
{
    public class VaccinationDto
    {
        public int Id { get; set; }
        public string? ApplicationOfVaccine { get; set; }
        public string? NameOfVaccine { get; set; }
        public string? VacineDose { get; set; }
        public string? VacineSeria { get; set; }
        public DateTime DateOfVaccination { get; set; }
        public DateTime? VaccinationExpirationDate { get; set; }

        public int UserId { get; set; }
        public MedicalInstitutionDto? MedicalInstitutions { get; set; }
        public int? MedicalInstitutionId { get; set; }
    }
}
