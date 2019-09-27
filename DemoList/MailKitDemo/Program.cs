using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;

namespace MailKitDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            var messageToSend = new MimeMessage
            {
                Sender = new MailboxAddress("dev1", "raikay@163.com"),
                Subject = "主题",
            };
            //多个发件人
            //messageToSend.From.Add(new MailboxAddress("dev2", "xiaoshi718@126.com"));
            //messageToSend.From.Add(new MailboxAddress("dev3", "xiaoshi719@126.com"));
            //邮件正文 HTML
            //messageToSend.Body = new TextPart(TextFormat.Html) { Text = "<p align=\"center\">This is some text in a paragraph.</p>" };
            //邮件正文 纯文本
            messageToSend.Body = new TextPart(TextFormat.Plain) { Text = "内容" };
            //收件人
            messageToSend.To.Add(new MailboxAddress("raikay@163.com"));
            //抄送
            //messageToSend.Cc.Add(new MailboxAddress("抄送者Email地址"));


            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.MessageSent += (sender, args) => { /* args.Response*/ };

                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;

                smtp.Connect("smtp.163.com");

                smtp.Authenticate("raikay", "password");

                smtp.Send(messageToSend);

                smtp.Disconnect(true);


            }


            Console.WriteLine("Hello World!");
            /*MessageSent事件里可以通过args参数，获得服务器的响应信息，以便于记录Log。
              连接outlook.com的服务器需要设置为SecureSocketOptions.StartTls，不然会拒绝连接。
              对于其他服务器，可以试试 SecureSocketOptions.Auto*/
        }
    }
}
