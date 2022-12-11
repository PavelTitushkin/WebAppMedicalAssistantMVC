using MediatR;

namespace WebAppMedicalAssistant_Data.CQS.Commands
{
    public class RemoveRefreshTokenCommand : IRequest
    {
        public Guid TokenValue;
    }
}
