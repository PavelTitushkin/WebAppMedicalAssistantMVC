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

        public virtual List<Disease> Diseases { get; set; }
        public virtual List<PrescribedMedication> PrescribedMedications { get; set; }
        public virtual List<PhysicalTherapy> PhysicalTherapies { get; set; }
        public virtual DoctorVisit DoctorVisit { get; set; }
        public int? DoctorVisitId { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }

    }
}
