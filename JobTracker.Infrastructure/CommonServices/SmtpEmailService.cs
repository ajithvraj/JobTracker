using JobTracker.Application.CommonInterfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Infrastructure.CommonServices
{
    public class SmtpEmailService : IEmailServices
    {

        private readonly IConfiguration _config;

        public SmtpEmailService(IConfiguration config)
        {
            _config = config;

        }

        public async Task SendAsync(string to , string subject, string body)
        {
            var smtp = new SmtpClient(
            _config["Email:SmtpServer"],
            int.Parse(_config["Email:SmtpPort"]!)
        )
            {
                Credentials = new NetworkCredential(
                _config["Email:SmtpUser"],
                _config["Email:SmtpPass"]
            ),
                EnableSsl = true
            };

            var mail = new MailMessage(
                _config["Email:SmtpUser"],
                to,
                subject,
                body
            );

            await smtp.SendMailAsync(mail);



        }
    }
}
