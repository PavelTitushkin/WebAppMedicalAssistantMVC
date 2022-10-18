namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class MedicalInstitution : IBaseEntity
    {
        public int Id { get; set; }
        public string NameMedicalInstitution { get; set; }
        public string Adress { get; set; }
        public string? OperatingMode { get; set; }
        public string? Contact { get; set; }

        public List<DoctorVisit> DoctorVisits { get; set; }
        public List<Vaccination> Vaccinations { get; set; }
        public List<Fluorography>? Fluorographys { get; set; }
        public List<Analysis> Analyses { get; set; }
        public List<MedicalExamination> medicalExaminations { get; set; }
        public List<PhysicalTherapy> physicalTherapies { get; set; }
    }
}
