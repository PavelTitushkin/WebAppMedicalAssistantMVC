using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppMedicalAssistant_Bussines.EmailService;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.Abstractions.EmailService;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_Data.CQS.Handlers.QueryHandlers;
using WebAppMedicalAssistant_Data.CQS.Queries;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistant_Bussines.ServicesImplementations
{
    public class AdminService : IAdminService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUserService _userService;

        public AdminService(IMapper mapper, IMediator mediator, IUserService userService)
        {
            _mapper = mapper;
            _mediator = mediator;
            _userService = userService;
        }

        public async Task<List<Message?>> GetUsersWithNearestDoctorVisitAsync()
        {
            try
            {
                var messages = await _mediator.Send(new GetUsersWithNearestDoctorVisitQuery() { dateTimeNow= DateTime.Now });

                return messages;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
