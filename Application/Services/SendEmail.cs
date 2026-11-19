using Application.Interfaces.IServices;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
//using System.Net.Mail;
using System.Text;

namespace Application.Services
{
    public class SendEmail : IsendEmail
    {
        public async Task<bool> sendemail(string to, string subject, string body)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("E_Commerce", "admin@ECommerce.com"));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;
                var bodybuilder = new BodyBuilder() { HtmlBody = body };
                email.Body = bodybuilder.ToMessageBody();

                var smtp = new SmtpClient();
                await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync("kareemelgendy50@gmail.com", "awto gnpy pzsa gozz");
                await smtp.SendAsync(email);
                return true;
            }catch 
            {
                return false;
            }
        }
    }
}
