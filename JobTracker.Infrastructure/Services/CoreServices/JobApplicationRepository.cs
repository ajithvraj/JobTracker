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
    public  class JobApplicationRepository : IJobApplicationRepository
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
            




    }
}
