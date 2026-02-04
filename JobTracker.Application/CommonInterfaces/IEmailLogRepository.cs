using JobTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.CommonInterfaces
{
    public interface IEmailLogRepository
    {

        Task AddAsync(EmailLog log);
        Task<bool> FollowUpExistAsync(int jobApplicationId);
        Task<List<EmailLog>> GetJobApplicationIdAsyc(int jobApplicationId);

    }
}
