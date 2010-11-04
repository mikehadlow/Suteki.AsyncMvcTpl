using System.Net.Mail;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace Suteki.AsyncMvcTpl.Services
{
    public class EmailService
    {
        public Task SendEmail(string emailAddress, string message)
        {
            var mailMessage = new MailMessage("info@suteki.co.uk", emailAddress)
            {
                Subject = "Hello!",
                Body = "An important message :)"
            };

            var smtpClient = new SmtpClient("smtp.suteki.co.uk");
            return smtpClient.SendTask(mailMessage, null);
        }
    }
}