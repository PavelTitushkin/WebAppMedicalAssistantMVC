namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class Doctor : IBaseEntity
    {
        public int Id { get; set; }
        public string FullNameDoctor { get; set; }
        public string Specializacion { get; set; }
        public float? Rating { get; set; }

        public List<DoctorVisit> DoctorVisits { get; set; }
    }
}
