using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;

namespace Lun2Code.Services
{
    public class EmailService
    {
        
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            
            emailMessage.From.Add(new MailboxAddress("Administration", "lun2code@kek.ru"));
            emailMessage.To.Add(new MailboxAddress("Email confirmation", email));

            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.yandex.ru", 228, false).ConfigureAwait(true);
                await client.AuthenticateAsync("lun2code@kek.ru", "pass").ConfigureAwait(true);
                await client.SendAsync(emailMessage).ConfigureAwait(true);
                await client.DisconnectAsync(true);
            }
            
        }
    }
}