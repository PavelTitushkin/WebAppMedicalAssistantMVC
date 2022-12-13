using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppMedicalAssistant_Core.Abstractions.EmailService;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_Data.CQS.Queries;
using WebAppMedicalAssistant_DataBase;
using MimeKit;

namespace WebAppMedicalAssistant_Data.CQS.Handlers.QueryHandlers
{
    public class GetUsersWithNearestDoctorVisitQueryHandler : IRequestHandler<GetUsersWithNearestDoctorVisitQuery, List<Message?>>
    {
        private readonly MedicalAssistantContext _context;
        private readonly IMapper _mapper;

        public GetUsersWithNearestDoctorVisitQueryHandler(MedicalAssistantContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Message?>> Handle(GetUsersWithNearestDoctorVisitQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var userDto = await _context.Users
                    .Include(e => e.DoctorVisits)
                    .ThenInclude(e => e.Doctor)
                    .Include(e => e.DoctorVisits)
                    .ThenInclude(e => e.MedicalInstitution)
                    .Where(e => e.RoleId == 1)
                    .Select(e => _mapper.Map<UserDto>(e))
                    .ToListAsync();

                var listMessage = new List<Message>();
                var dateSearch = request.dateTimeNow = DateTime.Now.AddDays(1);
                foreach (var user in userDto)
                {
                    if (user.DoctorVisits.Any())
                    {
                        foreach (var doctorVisit in user.DoctorVisits)
                        {
                            if (doctorVisit.DateVisit <= dateSearch && doctorVisit.DateVisit >= DateTime.Now)
                            {
                                listMessage.Add(new Message(
                                    new MailboxAddress("", user.Email),
                                    "Напоминание",
                                    $"{doctorVisit.DateVisit.ToString()} запись к врачу {doctorVisit.Doctor.FullNameDoctor} в {doctorVisit.MedicalInstitution.NameMedicalInstitution}"));
                            }

                        }
                    }
                }

                return listMessage;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
