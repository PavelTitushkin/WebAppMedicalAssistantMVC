using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using WebAppMedicalAssistant_Core.Abstractions.EmailService;

namespace WebAppMedicalAssistant_Bussines.EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfiguration;

        public EmailSender(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public async Task SendRangeEmailAsync(List<Message> message)
        {
            try
            {
                foreach (var item in message)
                {
                    var emailMessage = CreateEmailMessage(item);

                    await SendAsync(emailMessage);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("", _emailConfiguration.From));
            emailMessage.To.Add(message.AdressTo);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfiguration.UserName, _emailConfiguration.Password);

                    await client.SendAsync(mailMessage);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}
