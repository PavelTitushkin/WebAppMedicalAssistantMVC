namespace WebAppMedicalAssistant_Core.Abstractions.EmailService
{
    public interface IEmailService
    {
        Task GetUsersWithNearestDoctorVisitAsync();
    }
}
