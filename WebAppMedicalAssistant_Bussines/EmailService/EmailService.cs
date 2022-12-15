using MediatR;
using WebAppMedicalAssistant_Core.Abstractions.EmailService;
using WebAppMedicalAssistant_Data.CQS.Queries;

namespace WebAppMedicalAssistant_Bussines.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IMediator _mediator;
        private readonly IEmailSender _emailSender;

        public EmailService(IMediator mediator, IEmailSender emailSender)
        {
            _mediator = mediator;
            _emailSender = emailSender;
        }

        public async Task GetUsersWithNearestDoctorVisitAsync()
        {
            try
            {
                var messages = await _mediator.Send(new GetUsersWithNearestDoctorVisitQuery() { dateTimeNow = DateTime.Now });
                await _emailSender.SendRangeEmailAsync(messages);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
