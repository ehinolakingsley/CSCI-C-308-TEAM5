using System.Net.Mail;
using CSCI_308_TEAM5.API.Services.Config;

namespace CSCI_308_TEAM5.API.Services.Email
{
    sealed class EmailServices(IConfigService configService) : IEmailServices
    {
        public void postMail(string emailAddress, string message, string subject)
        {
            using (MailMessage mail = new MailMessage(new MailAddress(configService.emailClientCredential.SMTPAddress), new MailAddress(emailAddress)))
            {
                using (SmtpClient smtp = new SmtpClient(configService.emailClientCredential.SMTPServer, configService.emailClientCredential.SMTPPort))
                {
                    mail.IsBodyHtml = false;
                    mail.Subject = subject;
                    mail.Body = message;

                    smtp.Credentials = new NetworkCredential(configService.emailClientCredential.SMTPAddress, configService.emailClientCredential.SMTPPwd);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}
