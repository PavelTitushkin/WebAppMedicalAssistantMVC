namespace WebAppMedicalAssistantAPI.Model.Responses
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public string Role { get; set; }
        public int UserId { get; set; }
        public DateTime TokenExpiration { get; set; }
        public Guid RefreshToken { get; set; }
    }
}
