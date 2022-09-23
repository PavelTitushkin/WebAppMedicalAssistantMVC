namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class User : IBaseEntity
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        //навигационные свойства
        public virtual List<Analysis> Analyzes { get; set; }
        public virtual List<DoctorVisit> DoctorVisits { get; set; }
        public virtual List<MedicalExamination> MedicalExaminations { get; set; }
        public virtual List<Vaccination> Vaccinations { get; set; }
        public virtual List<Fluorography> Fluorographies { get; set; }
        public virtual List<PrescribedMedication> PrescribedMedication { get; set; }
        public virtual List<TransferredDisease> TransferredDiseases { get; set; }
    }
}
