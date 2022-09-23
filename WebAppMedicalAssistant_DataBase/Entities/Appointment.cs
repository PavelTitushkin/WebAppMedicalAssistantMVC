namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class Appointment: IBaseEntity
    {
        public int Id { get; set; }
        public string DescriptionOfDestination { get; set; }


        public virtual List<MedicalExamination> MedicalExaminations { get; set; }
        public virtual List<PhysicalTherapy> PhysicalTherapys { get; set; }
        public virtual List<PrescribedMedication> PrescribedMedications { get; set; }
        public virtual DoctorVisit DoctorVisit { get; set; }
        public int? DoctorVisitId { get; set; }
    }
}
