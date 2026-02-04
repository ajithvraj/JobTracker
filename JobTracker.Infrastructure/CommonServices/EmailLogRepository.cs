using JobTracker.Application.CommonInterfaces;
using JobTracker.Domain.Entities;
using JobTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Infrastructure.CommonServices
{
    public class EmailLogRepository : IEmailLogRepository
    {

        private readonly JobTrackerDbContext _db;

        public EmailLogRepository(JobTrackerDbContext db)
        {

            _db = db;
        }

        public async Task AddAsync(EmailLog log)
        {
            _db.EmailLogs.Add(log);
            await _db.SaveChangesAsync();

        }

        public async Task<bool> FollowUpExistAsync(int jobApplicationId)
        {


            return await _db.EmailLogs.AnyAsync(x => x.JobApplicationId == jobApplicationId);
        }

        public async Task<List<EmailLog>> GetJobApplicationIdAsyc(int jobApplicationId)
        {

            return await _db.EmailLogs.Where(x => x.JobApplicationId == jobApplicationId).
                OrderByDescending(x => x.SentAt).
                ToListAsync();

        }
    }
}
