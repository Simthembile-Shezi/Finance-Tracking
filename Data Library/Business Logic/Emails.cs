using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Data_Library.Business_Logic
{
    public static class Emails
    {
        public static bool SendEmail(string email, string recever, string sub, string body)
        {
            try
            {
                var senderEmail = new MailAddress("Finance.Tracking@outlook.com", "Finance Tracking");
                var receiverEmail = new MailAddress(email, recever);

                var smtp = new SmtpClient
                {
                    Host = "smtp-mail.outlook.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, "Project2022")
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = sub,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
