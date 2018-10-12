using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibrary.SendEmail
{
    public class SendEmailDemo
    {
        public static void Run()
        {
            //host和端口号，根据服务类型查询对应邮件的设置
            SmtpClient SmtpServer = new SmtpClient("smtp.qq.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("sender@qq.com");
            //这里可以添加多个
            mail.To.Add("receiver@qq.com");
            //标题
            mail.Subject = "Test Mail - 1";
            mail.IsBodyHtml = true;
            string htmlBody;
            htmlBody = "Write some HTML code here";
            //内容
            mail.Body = htmlBody;
            //端口号
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            //身份认证
            //这里的密码是授权码，而非账号密码，在开启POP3/SMTP服务服务之后获得
            SmtpServer.Credentials = new System.Net.NetworkCredential("sender@qq.com", "password");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }
    }
}
