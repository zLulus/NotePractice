using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CodeLibraryForDotNetCore.SendEmail
{
    public class SendEmailDemo
    {
        public static void Run()
        {
            SmtpClient client = new SmtpClient("smtp.qq.com");
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("sender@qq.com", "password");

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("sender@qq.com");
            mailMessage.To.Add("receiver@qq.com");
            mailMessage.Body = "body";
            mailMessage.Subject = "subject";
            client.Send(mailMessage);
        }
    }
}
