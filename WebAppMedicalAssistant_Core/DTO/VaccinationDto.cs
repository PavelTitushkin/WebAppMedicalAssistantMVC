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

        public MedicalInstitutionDto? MedicalInstitutionsDto { get; set; }
        public UserDto? UserDto { get; set; }
    }
}
