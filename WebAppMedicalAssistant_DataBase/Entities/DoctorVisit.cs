namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class DoctorVisit : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime DateVisit { get; set; }
        public decimal PriceVisit { get; set; }

        //навигационные свойства
        public virtual List<MedicalInstitution> MedicalInstitutions { get; set; }
        public virtual List<Doctor> Doctors { get; set; }
        public virtual List<Appointment> Appointments { get; set; }
        public virtual List<TransferredDisease> TransferredDiseases { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}
