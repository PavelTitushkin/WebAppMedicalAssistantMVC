using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_Data.CQS.Queries;
using WebAppMedicalAssistant_DataBase;

namespace WebAppMedicalAssistant_Data.CQS.Handlers.QueryHandlers
{
    public class GetUserByRefreshTokenQueryHandler : IRequestHandler<GetUserByRefreshTokenQuery, UserDto?>
    {
        private readonly MedicalAssistantContext _context;
        private readonly IMapper _mapper;

        public GetUserByRefreshTokenQueryHandler(MedicalAssistantContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto?> Handle(GetUserByRefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var user = (await _context.RefreshTokens
                .Include(token => token.User)
                .ThenInclude(user => user.Roles)
                .AsNoTracking()
                .FirstOrDefaultAsync(token => token.Token == request.RefreshToken,
                    cancellationToken))?.User;

            return _mapper.Map<UserDto>(user);
        }
    }
}
