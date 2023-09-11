using System;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;

namespace MyApp_Auth
{
    public class Email
    {
        public static void Send(string displayname, string to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("mahdiwich86@gmail.com", displayname);
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.elasticemail.com");
            client.Port = 2525;
            client.Credentials = new NetworkCredential("mahdiwich86@gmail.com", "4753F57FFE0A279D18B4655228F1EC77DB35");
            client.EnableSsl = true;
            client.Send(mail);
        }

        public static string GetBaseUrl()
        {
            var request = HttpContext.Current.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/")
                appUrl = "/" + appUrl;

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

            return baseUrl;
        }
    }
}
