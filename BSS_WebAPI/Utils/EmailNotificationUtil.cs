using System.Net;
using System.Net.Mail;
using BSS_WebAPI.Models.DTO;
using BSS_WebAPI.Models;

namespace BSS_WebAPI.Utils
{
    public static class EmailNotificationUtil
    {
        private static string EmailHost { get; } = GlobalSettings.Instance.SmtpHost;
        private static string EmaiUser { get; } = GlobalSettings.Instance.SmtpUser; 
        private static string EmailPassword { get; } = GlobalSettings.Instance.SmtpPassword;
        private static bool IsSSL { get; } = GlobalSettings.Instance.SmtpIsSsl == "true";
        private static int Port { get; } = int.Parse(GlobalSettings.Instance.SmtpPort);

        public static bool Send(Email email)
        {
            using (var mail = new MailMessage(EmaiUser, email.To))
            {
                mail.Subject = email.Subject;
                mail.Body = email.Body;
                mail.IsBodyHtml = true;
                var smtp = new SmtpClient();
                smtp.Host = EmailHost;
                smtp.EnableSsl = IsSSL;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(EmaiUser, EmailPassword);
                smtp.Port = Port;
                smtp.Send(mail);
                return true;
            }
        }
    }
}