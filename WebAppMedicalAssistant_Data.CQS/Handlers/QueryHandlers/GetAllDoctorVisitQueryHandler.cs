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
    public class GetAllDoctorVisitQueryHandler:IRequestHandler<GetAllDoctorVisitQuery, List<DoctorVisitDto?>>
    {
        private readonly MedicalAssistantContext _context;
        private readonly IMapper _mapper;

        public GetAllDoctorVisitQueryHandler(MedicalAssistantContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<DoctorVisitDto?>> Handle(GetAllDoctorVisitQuery request, CancellationToken cancellationToken)
        {
            try
            {
                     var dto = await _context.DoctorVisits
                    .AsNoTracking()
                    .Where(user => user.UserId.Equals(request.Id))
                    .Include(entity => entity.MedicalInstitution)
                    .Include(entity => entity.Doctor)
                    .Include(entity => entity.TransferredDisease)
                    .ThenInclude(tranDis => tranDis.Disease)
                    .Include(entity => entity.Appointment)
                    .OrderBy(entity => entity.DateVisit)
                    .Select(entity => _mapper.Map<DoctorVisitDto>(entity))
                    .ToListAsync();

                return dto;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
