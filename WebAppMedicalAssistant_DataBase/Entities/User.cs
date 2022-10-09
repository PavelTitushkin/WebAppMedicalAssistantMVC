namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class User : IBaseEntity
    {
        public int Id { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? Patronymic { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        //навигационные свойства
        public Role Roles { get; set; }
        public int RoleId { get; set; }
        public List<Analysis>? Analyzes { get; set; }
        public List<DoctorVisit>? DoctorVisits { get; set; }
        public List<MedicalExamination>? MedicalExaminations { get; set; }
        public List<Vaccination>? Vaccinations { get; set; }
        public List<Fluorography>? Fluorographies { get; set; }
        public List<PrescribedMedication>? PrescribedMedication { get; set; }
        public List<TransferredDisease>? TransferredDiseases { get; set; }
    }
}
