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

        public virtual List<MedicalInstitution> MedicalInstitutions { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}
