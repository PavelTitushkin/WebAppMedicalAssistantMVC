namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class MedicalInstitution : IBaseEntity
    {
        public int Id { get; set; }
        public string NameMedicalInstitution { get; set; }
        public string Adress { get; set; }
        public string? OperatingMode { get; set; }
        public string? Contact { get; set; }

        //навигационное свойство
        public virtual DoctorVisit DoctorVisit { get; set; }
        public int? DoctorVisitId { get; set; }
        public virtual Vaccination Vaccination { get; set; }
        public int? VaccinationId { get; set; }

        public virtual List<Fluorography> Fluorographys { get; set; }
        public virtual List<Analysis> Analyses { get; set; }
    }
}
