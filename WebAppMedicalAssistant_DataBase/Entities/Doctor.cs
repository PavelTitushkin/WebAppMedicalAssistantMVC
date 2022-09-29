namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class Doctor : IBaseEntity
    {
        public int Id { get; set; }
        public string LastNameDoctor { get; set; }
        public string FirstNameDoctor { get; set; }
        public string PatronymicDoctor { get; set; }
        public string Specializacion { get; set; }
        public float? Rating { get; set; }

        public List<DoctorVisit> DoctorVisits { get; set; }
    }
}
