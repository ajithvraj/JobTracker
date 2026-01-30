using JobTracker.Application.Repository.CoreRepository;
using JobTracker.Domain.Entities;
using JobTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Infrastructure.Services.CoreServices
{
    public class EmailTemplateRepository : IEmailTemplateRepository
    {

        private readonly JobTrackerDbContext _db;
        public EmailTemplateRepository(JobTrackerDbContext db)
        { 
            _db = db;
        }

        public async Task<EmailTemplate> GetDefaultFollowUpAsync() 
        {

            return await _db.EmailTemplates.AsNoTracking().Where(x => !x.IsDeleted && x.IsDefault).FirstAsync();



        }

    }
}
