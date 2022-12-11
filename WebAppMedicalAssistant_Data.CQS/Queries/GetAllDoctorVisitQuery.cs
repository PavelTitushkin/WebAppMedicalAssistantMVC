using MediatR;
using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Data.CQS.Queries
{
    public class GetAllDoctorVisitQuery : IRequest<List<DoctorVisitDto?>>
    {
        public int Id { get; set; }
    }
}
