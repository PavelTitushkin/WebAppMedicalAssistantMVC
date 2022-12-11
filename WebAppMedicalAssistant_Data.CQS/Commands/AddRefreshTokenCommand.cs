using MediatR;

namespace WebAppMedicalAssistant_Data.CQS.Commands
{
    public class AddRefreshTokenCommand : IRequest
    {
        public Guid TokenValue;
        public int UserId;
    }
}
