namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class Appointment: IBaseEntity
    {
        public int Id { get; set; }
        public string DescriptionOfDestination { get; set; }


        public List<MedicalExamination> MedicalExaminations { get; set; }
        public List<PhysicalTherapy> PhysicalTherapys { get; set; }
        public List<PrescribedMedication> PrescribedMedications { get; set; }
        public DoctorVisit? DoctorVisit { get; set; }
        public int? DoctorVisitId { get; set; }
    }
}
