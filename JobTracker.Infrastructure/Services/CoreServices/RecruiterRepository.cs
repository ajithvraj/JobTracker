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
    public class RecruiterRepository : IRecruiterRepository
    {

        private readonly JobTrackerDbContext _db;

        public RecruiterRepository(JobTrackerDbContext db) 
        {
            _db = db;
        } 


        public async Task<Recruiter?>GetByEmail(string email)
        {
            return await _db.Recruiters.FirstOrDefaultAsync(x => x.Email == email);

        } 


        public async Task<Recruiter> CreateAsync(string? name , string email, int commpanyId)
        {

            var recruiter = new Recruiter
            { 
                Name = name,
                Email = email,
                CompanyId = commpanyId

            };


            _db.Recruiters.Add(recruiter);
            await _db.SaveChangesAsync();
            return  recruiter;

        }


    }
}
