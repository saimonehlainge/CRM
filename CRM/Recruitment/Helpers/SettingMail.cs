using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Recruitment.Data;
using System.Net.Mail;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
namespace Recruitment.Helpers
{
    public class SettingMail
    {
        public IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

        public async Task<bool> SendMail(string EmailSetting, string mail, string subject, string body, string webroot)
        {
            try
            {
                var configuration = GetConfiguration();
                string Email = configuration.GetSection("MailSettings:Mail").Value;
                string Password = configuration.GetSection("MailSettings:Password").Value;
                string Smtp = configuration.GetSection("MailSettings:Host").Value;
                int SmtpPort = Convert.ToInt32(configuration.GetSection("MailSettings:Port").Value);
                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient(Smtp, SmtpPort);

                //smtpClient.EnableSsl = false;
                smtpClient.Timeout = 10000;
                smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                //smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(Email, Password);

                MailMessage mailMessage = new MailMessage(Email!, EmailSetting, subject, body);
                mailMessage.IsBodyHtml = true;
                //mailMessage.CC.Add(cc);
                mailMessage.BodyEncoding = System.Text.UTF8Encoding.UTF8;

                var pathImage = webroot + "\\images\\brand book - Recruitment89_FINAL-26.jpg";
                LinkedResource inline_post_customer = new LinkedResource(pathImage);
                inline_post_customer.ContentId = "slipfile";
                mailMessage.Attachments.Add(new Attachment(pathImage!));
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception error)
            {
                return false;
            }
        }
    }
}
