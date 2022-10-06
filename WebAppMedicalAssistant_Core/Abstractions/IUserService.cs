using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Core.Abstractions
{
    public interface IUserService
    {
        Task<bool> CheckUserPassword(string email, string password);
        Task<UserDto> GetUserByEmailAsync(string email);
        Task<int> RegisterUser(UserDto user);
    }
}
