namespace WebAppMedicalAssistantAPI.Model.Requests
{
    public class RegisterUserRequestModel
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
