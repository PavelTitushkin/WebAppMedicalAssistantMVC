namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class MedicalExamination : IBaseEntity
    {
        public int Id { get; set; }
        public string NameOfMedicalExamination { get; set; }
        public DateTime DateOfMedicalExamination { get; set; }
        public decimal? PriceOfMedicalExamination { get; set; }
        public byte[]? ScanOfMedicalExamination { get; set; }

        public virtual Appointment Appointment { get; set; }
        public int? AppointmentId { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}
