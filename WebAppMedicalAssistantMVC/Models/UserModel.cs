namespace WebAppMedicalAssistantMVC.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public DateTime? Birthday { get; set; }
        public IFormFile? Avatar { get; set; }
        public byte[]? AvatarByte { get; set; }

        public string? Email { get; set; }
    }
}
