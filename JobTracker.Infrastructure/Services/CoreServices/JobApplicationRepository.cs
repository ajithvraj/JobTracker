using Dapper;
using JobTracker.Application.DTOs.CoreDTOs;
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
    public class JobApplicationRepository : IJobApplicationRepository
    {

        private readonly JobTrackerDbContext _db;

        public JobApplicationRepository(JobTrackerDbContext db)
        {

            _db = db;
        }

        public async Task<int> CreateAsync(JobApplication application)
        {

            _db.JobApplications.Add(application);
            await _db.SaveChangesAsync();
            return application.Id;



        }
        public async Task<List<JobApplicationResponseDto>> GetAllByUserAsync(int userId)
        {

            using var connection = _db.Database.GetDbConnection();


            var result = await connection.QueryAsync<JobApplicationResponseDto>(

                "GetJobApplicationsByUser", new { UserId = userId }, commandType: System.Data.CommandType.StoredProcedure

                );

            return result.ToList();
        }
             public async Task<JobApplication> GetWithDetailsAsync(int jobApplicationId)
        {
            return await _db.JobApplications
                .Include(x => x.Company)
                .Include(x => x.Recruiter)
                .Include(x => x.User)
                .FirstAsync(x => x.Id == jobApplicationId);
        }

        public async Task UpdateAsync(JobApplication application)
        {
            _db.JobApplications.Update(application);
            await _db.SaveChangesAsync();
        }

        public async Task<List<JobApplication>> GetDueEmailFollowUpsAsync(DateTime dueDate)
        {

            return await _db.JobApplications.Where(x => x.Source == Domain.Enums.ApplicationEnums.ApplicationSource.Email && x.Status == Domain.Enums.ApplicationEnums.ApplicationStatus.Applied && x.AppliedDate <= dueDate && x.LastContactDate == null).ToListAsync();


        }








    }
}

