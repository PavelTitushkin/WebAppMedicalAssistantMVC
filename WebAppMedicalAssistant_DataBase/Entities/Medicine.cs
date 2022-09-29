namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class Medicine : IBaseEntity
    {
        public int Id { get; set; }
        public string NameOfMedicine { get; set; }
        public string ShortDescriptionOfMedicine { get; set; }

        public List<PrescribedMedication> PrescribedMedications { get; set; }
    }
}
