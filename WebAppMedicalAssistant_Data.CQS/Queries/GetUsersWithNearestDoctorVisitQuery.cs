using MediatR;
using WebAppMedicalAssistant_Core.Abstractions.EmailService;

namespace WebAppMedicalAssistant_Data.CQS.Queries
{
    public class GetUsersWithNearestDoctorVisitQuery : IRequest<List<Message?>>
    {
        public DateTime dateTimeNow;
    }
}
