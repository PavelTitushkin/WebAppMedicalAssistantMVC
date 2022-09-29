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

        public List<Disease> Diseases { get; set; }
        public List<PrescribedMedication> PrescribedMedications { get; set; }
        public List<PhysicalTherapy> PhysicalTherapies { get; set; }
        public List<DoctorVisit> DoctorVisits { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

    }
}
