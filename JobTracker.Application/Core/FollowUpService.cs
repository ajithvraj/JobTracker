using JobTracker.Application.CommonInterfaces;
using JobTracker.Application.Core.Services;
using JobTracker.Application.DTOs.CoreDTOs.FollowUpDTOs;
using JobTracker.Application.Repository.CoreRepository;
using JobTracker.Domain.Entities;
using JobTracker.Domain.Enums.ApplicationEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Reflection.Metadata.BlobBuilder;

namespace JobTracker.Application.Core
{
    public class FollowUpService : IFollowUpService
    {
        private readonly IJobApplicationRepository _application;
        private readonly IEmailServices _email;
        private readonly IEmailTemplateRepository _templates;
        private readonly ITemplateRenderer _renderer;
        private readonly IEmailLogRepository _log;
        private readonly IApplicationStatus _status;

        public FollowUpService(IJobApplicationRepository application, IEmailServices email, IEmailTemplateRepository templates, ITemplateRenderer renderer, IEmailLogRepository log , IApplicationStatus status)
        {
            _application = application;
            _email = email;
            _templates = templates;
            _renderer = renderer;
            _log = log;
            _status = status;
        }

        public async Task SendFollowUpAsync(int jobApplicationId, int userId)
        {
            //  Load application
            var app = await _application.GetWithDetailsAsync(jobApplicationId);

            Console.WriteLine($"Source value: {(int)app.Source}");
            Console.WriteLine($"Source enum type: {app.Source.GetType().FullName}");
            Console.WriteLine($"Email enum type: {ApplicationSource.Email.GetType().FullName}");


            Console.WriteLine($"Source from DB: {(int)app.Source}");
            Console.WriteLine($"Email enum value: {(int)ApplicationSource.Email}");



            if (app.Source != ApplicationSource.Email)
                throw new Exception("Follow-up allowed only for email applications");

            if (app.Status == ApplicationStatus.FollowUpSent)
                throw new Exception("Follow-up already sent");

    

            if (await _log.FollowUpExistAsync(app.Id))
                throw new Exception("Follow-up already logged");

            // Load template
            var template = await _templates.GetDefaultFollowUpAsync();

            //  Render
            var values = new Dictionary<string, string>
            {
                ["Role"] = app.Role ?? "",
                ["CompanyName"] = app.Company!.Name,
                ["RecruiterName"] = app.Recruiter?.Name ?? "Hiring Team",
                ["AppliedDate"] = app.AppliedDate.ToString("dd MMM yyyy"),
                ["UserName"] = app.User!.FullName
            };

            var subject = _renderer.Render(template.Subject, values);
            var body = _renderer.Render(template.Body, values);

            // Send email
            await _email.SendAsync(userId, app.Recruiter!.Email, subject, body);

            //  Log email
            await _log.AddAsync(new EmailLog
            {
                JobApplicationId = app.Id,
                ToEmail = app.Recruiter.Email,
                Subject = subject,
                Body = body,
                SentAt = DateTime.UtcNow
            });

            //  Update status
            app.Status = ApplicationStatus.FollowUpSent;
            app.LastContactDate = DateTime.UtcNow;

            await _application.UpdateAsync(app);



        }

        public async Task<List<FollowUpHistoryDto>> GetFollowHistoryAsync(int jobApplicationId)
        {
            var logs = await _log.GetJobApplicationIdAsyc(jobApplicationId);

            return logs.Select(x => new FollowUpHistoryDto
            {
                SentAt = x.SentAt,
                ToEmail = x.ToEmail,
                Subject = x.Subject,
                Body = x.Body


            }).ToList();

        }

        public async Task UpdateStatusAsync(int jobApplicationId, ApplicationStatus status)
        {

            var app = await _status.GetByIdAsync(jobApplicationId);
            if (app == null) { throw new Exception("No job application found"); }


            app.Status = status;
            app.UpdatedAt = DateTime.UtcNow;

            if (app.Status != ApplicationStatus.Applied) app.LastContactDate = DateTime.UtcNow; 

            await _status.UpdateAsync(app);





        }
    }
}
