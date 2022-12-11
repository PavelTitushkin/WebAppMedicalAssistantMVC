using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppMedicalAssistant_Data.CQS.Commands;
using WebAppMedicalAssistant_DataBase;

namespace WebAppMedicalAssistant_Data.CQS.Handlers.CommandHandlers
{
    public class RemoveRefreshTokenCommandHandler : IRequestHandler<RemoveRefreshTokenCommand, Unit>
    {
        private readonly MedicalAssistantContext _context;

        public RemoveRefreshTokenCommandHandler(MedicalAssistantContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RemoveRefreshTokenCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var refreshToken = await _context.RefreshTokens
                    .FirstOrDefaultAsync(rt => command.TokenValue.Equals(rt.Token), cancellationToken);
                _context.RefreshTokens.Remove(refreshToken);
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
