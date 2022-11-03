using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Core.Abstractions
{
    public interface IUserService
    {
        Task<bool> CheckUserPasswordAsync(string email, string password);
        Task<bool> IsUserExistAsync(string email);
        Task<UserDto> GetUserByEmailAsync(string email);
        Task<int> RegisterUser(UserDto user);
        Task<List<UserDto>> GetAllUserAsync();
        Task DeleteUserAsync(int id);
    }
}
