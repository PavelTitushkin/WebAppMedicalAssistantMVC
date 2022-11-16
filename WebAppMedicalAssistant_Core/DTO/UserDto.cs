namespace WebAppMedicalAssistant_Core.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public DateTime? Birthday { get; set; }
        public byte[]? Avatar { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }
    }
}
