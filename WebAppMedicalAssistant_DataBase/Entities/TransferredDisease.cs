using WebAppMedicalAssistant_Core;

namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class TransferredDisease : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime DateOfDisease { get; set; }
        public DateTime? DateOfRecovery { get; set; }
        public bool TypeOfTreatment { get; set; }   
        public FormOfTransferredDisease FormOfTransferredDisease { get; set; }

        public Disease Disease { get; set; }
        public int DiseaseId { get; set; }
        public List<DoctorVisit>? DoctorVisits { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
