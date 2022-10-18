namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class Disease : IBaseEntity
    {
        public int Id { get; set; }
        public string NameOfDisease { get; set; }
        public string ShotDescriptionDisease { get; set; }

        public List<TransferredDisease> TransferredDiseases { get; set; }
    }
}
