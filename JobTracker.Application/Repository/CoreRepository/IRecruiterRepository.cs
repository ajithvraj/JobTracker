using JobTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.Repository.CoreRepository
{
    public interface IRecruiterRepository
    {
        Task<Recruiter?> GetByEmail(string email);
        Task<Recruiter> CreateAsync(string? name, string email, int companyId);

    }
}
