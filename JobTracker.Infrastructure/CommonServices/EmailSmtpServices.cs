using JobTracker.Application.CommonInterfaces;
using JobTracker.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Infrastructure.CommonServices
{
    public class EmailSmtpServices : IEmailServices
    {
        private readonly JobTrackerDbContext _db;
        private readonly IEncryptionService _ecryption;

        public EmailSmtpServices(

            JobTrackerDbContext db, IEncryptionService ecryption)
        {
            _db = db;
            _ecryption = ecryption;
        }

        public async Task SendAsync(int userId , string to , string subject, string body )
        {
             
            var user = await _db.Users.FindAsync(userId);

            if (user == null) throw new Exception("User not found");

            if (string.IsNullOrEmpty(user.SmtpPassword)) throw new Exception("SMTP settings not configured");
            var smtpPassword = _ecryption.Decrypt(user.SmtpPassword);

            //here we create  smtp client

            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(user.Email,smtpPassword),
                EnableSsl = true

            };

            //sending email from user 

            var email = new MailMessage(from : user.Email , to : to , subject : subject , body : body);
            await smtp.SendMailAsync(email);

        }


        
    }
}
