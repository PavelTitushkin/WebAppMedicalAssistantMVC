namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class Role : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}
