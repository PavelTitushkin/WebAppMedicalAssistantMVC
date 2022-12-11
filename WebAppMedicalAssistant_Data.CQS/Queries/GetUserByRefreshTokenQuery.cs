using MediatR;
using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Data.CQS.Queries
{
    public class GetUserByRefreshTokenQuery : IRequest<UserDto?>
    {
        public Guid RefreshToken { get; set; }
    }
}
