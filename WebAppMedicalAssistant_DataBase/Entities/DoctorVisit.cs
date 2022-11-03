namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class DoctorVisit : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime DateVisit { get; set; }
        public decimal? PriceVisit { get; set; }

        public Appointment Appointment { get; set; }
        public MedicalInstitution? MedicalInstitution { get; set; }
        public int? MedicalInstitutionId { get; set; }
        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }
        public TransferredDisease? TransferredDisease { get; set; }
        public int? TransferredDiseaseId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
