namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class Vaccination : IBaseEntity
    {
        public int Id { get; set; }
        public string ApplicationOfVaccine { get; set; }
        public string NameOfVaccine { get; set; }
        public string VacineDose { get; set; }
        public string? VacineSeria { get; set; }
        public DateTime DateOfVaccination { get; set; }
        public DateTime? VaccinationExpirationDate { get; set; }

        public MedicalInstitution? MedicalInstitutions { get; set; }
        public int? MedicalInstitutionId { get; set; } 
        public  User? User { get; set; }
        public int? UserId { get; set; }
    }
}
