using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WorkFlowX.Models;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using System.Web.Helpers;

namespace WorkFlowX.Services
{
    public static class MailService
    {
        public static string _host = "smtp.gmail.com";
        public static int _port = 587;

        public static string _username = "WorkFlowX";
        public static string _usermail = "workflowxproyect@gmail.com";
        public static string _password = "jrtjcnlctgertzot";

        public static bool SendMail(DtoMail mail)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_username, _usermail));
                message.To.Add(MailboxAddress.Parse(mail.MailTo));
                message.Subject = mail.MailSubject;
                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = mail.MailBody
                };
                var smtp = new SmtpClient();
                smtp.CheckCertificateRevocation = false;
                smtp.Connect(_host, _port, SecureSocketOptions.Auto);

                smtp.Authenticate(_usermail, _password);
                smtp.Send(message);
                smtp.Disconnect(true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}