using MediatR;
using WebAppMedicalAssistant_Data.CQS.Commands;
using WebAppMedicalAssistant_DataBase;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistant_Data.CQS.Handlers.CommandHandlers
{
    public class AddRefreshTokenCommandHandler : IRequestHandler<AddRefreshTokenCommand, Unit>
    {
        private readonly MedicalAssistantContext _context;

        public AddRefreshTokenCommandHandler(MedicalAssistantContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddRefreshTokenCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var refToken = new RefreshTokens()
                {
                    Token = command.TokenValue,
                    UserId = command.UserId
                };
                await _context.RefreshTokens.AddAsync(refToken, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
