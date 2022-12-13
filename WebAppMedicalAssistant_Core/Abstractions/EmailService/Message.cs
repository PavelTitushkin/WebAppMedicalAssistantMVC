using MimeKit;

namespace WebAppMedicalAssistant_Core.Abstractions.EmailService
{
    public class Message
    {
        public MailboxAddress AdressTo { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public Message(MailboxAddress adressTo, string subject, string content)
        {
            AdressTo = adressTo;
            //To.AddRange(to.Select(x => new MailboxAddress("", x)));
            Subject = subject;
            Content = content;
        }
    }
}
