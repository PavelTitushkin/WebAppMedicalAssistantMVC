namespace WebAppMedicalAssistant_Core.Abstractions.EmailService
{
    public interface IEmailSender
    {
        //Task SendEmailAsync(Message email);
        Task SendRangeEmailAsync(List<Message> message);

    }
}
