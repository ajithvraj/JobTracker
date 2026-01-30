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
    public class ResumeRepository :  IResumeRepository
    {
        private readonly JobTrackerDbContext _db;
        public ResumeRepository(JobTrackerDbContext db)
        {
            _db = db;
        }

        public async Task AddResumeAsync(Resume resume)
        {
            _db.Resumes.Add(resume);
            await _db.SaveChangesAsync();

        }

        public async Task<List<Resume>> GetByUserIdAsync(int userId)
        {
            return await _db.Resumes.Where(r => r.UserId == userId).OrderByDescending(r => r.CreatedAt).ToListAsync();

        }



    }
}
