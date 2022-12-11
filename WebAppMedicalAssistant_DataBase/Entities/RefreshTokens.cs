namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class RefreshTokens : IBaseEntity
    {
        public int Id { get; set; }
        public Guid Token { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
