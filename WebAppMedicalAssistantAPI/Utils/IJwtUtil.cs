using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistantAPI.Model.Responses;

namespace WebAppMedicalAssistantAPI.Utils
{
    public interface IJwtUtil
    {
        Task<TokenResponse> GenerateTokenAsync(UserDto? dto);
        Task RemoveRefreshTokenAsync(Guid refreshToken);
    }
}
